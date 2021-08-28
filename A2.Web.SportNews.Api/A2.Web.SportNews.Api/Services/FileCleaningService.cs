using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using A2.Web.SportNews.Database;
using A2.Web.SportNews.Options;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace A2.Web.SportNews.Api.Services
{
    public class FileCleaningService : BackgroundService
    {
        private readonly AppOptions _options;
        private readonly UnitOfWorkFactory _uoFactory;

        public FileCleaningService(AppOptions options, UnitOfWorkFactory uoFactory)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
            _uoFactory = uoFactory ?? throw new ArgumentNullException(nameof(uoFactory));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var delay = TimeSpan.FromMinutes(_options.FileCleaningMinutesTimeout);
            while (!stoppingToken.IsCancellationRequested)
            {
                await CleanFilesAsync();
                await Task.Delay(delay, stoppingToken);
            }
        }

        private async Task CleanFilesAsync()
        {
            try
            {
                Log.Information($"{nameof(FileCleaningService)}: cleaning has started");
                var fileNames = GetAllFiles();
                var unlinkedFileNames = (await GetNewsUnlinkedFiles(fileNames))
                    .Concat(await GetContactsUnlinkedFiles(fileNames))
                    .ToList();

                Log.Information($"{nameof(FileCleaningService)}: there are {unlinkedFileNames.Count} unlinked files.");

                if (unlinkedFileNames.Count > 0)
                {
                    Log.Information(
                        $"{nameof(FileCleaningService)}: files to delete:\n{string.Join(",", unlinkedFileNames)}.");
                }
                else
                {
                    Log.Information($"{nameof(FileCleaningService)}: no unlinked files to clean found.");
                    return;
                }

                foreach (var file in unlinkedFileNames)
                {
                    var filePath = Path.Combine(_options.FileSaveAbsolutePath, file);
                    if (File.Exists(filePath))
                        File.Delete(filePath);
                }

                Log.Information($"{nameof(FileCleaningService)}: cleaning complete.");
            }
            catch (Exception e)
            {
                Log.Error($"{nameof(FileCleaningService)}: error occurred in cleaning process", e);
            }
        }

        private List<string> GetAllFiles()
        {
            var directory = new DirectoryInfo(_options.FileSaveAbsolutePath);
            return directory.GetFiles().Select(x => x.Name).ToList();
        }

        private async Task<IEnumerable<string>> GetNewsUnlinkedFiles(List<string> allFileNames)
        {
            await using var uow = _uoFactory.Create();
            var linkedFileNames = (await uow.NewsRepository.GetEntitiesAsync()).Select(x => x.ImageLink);

            return allFileNames.Where(x => !linkedFileNames.Contains(x));
        }

        private async Task<IEnumerable<string>> GetContactsUnlinkedFiles(List<string> allFileNames)
        {
            await using var uow = _uoFactory.Create();
            var linkedFileNames = (await uow.ContactsRepository.GetEntitiesAsync()).Select(x => x.ImageLink);

            return allFileNames.Where(x => !linkedFileNames.Contains(x));
        }
    }
}
