using System;
using System.IO;
using System.Threading.Tasks;
using A2.Web.SportNews.Options;
using Serilog;

namespace A2.Web.SportNews.Api.Services
{
    public class FileManageService
    {
        public const int MaxMbFileSize = 10;
        private readonly AppOptions _options;

        public FileManageService(AppOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task UploadAsync(string fileName, string fileB64)
        {
            var bytes = Convert.FromBase64String(fileB64);

            if (bytes.Length <= 0)
            {
                Log.Warning($"Transmitted file is empty. File size is 0Mb.");
                return;
            }

            if (bytes.Length > MaxMbFileSize * 1024 * 1024)
            {
                Log.Warning($"File is too large to load. The size restriction is {MaxMbFileSize}Mb");
                return;
            }

            var filePath = Path.Combine(new[] {_options.FileSaveAbsolutePath, fileName});

            if (filePath == null)
            {
                Log.Error($"Can not save file, the \"{nameof(_options.FileSaveAbsolutePath)}\" is null.");
                throw new ArgumentException(filePath);
            }

            await File.WriteAllBytesAsync(filePath, bytes);
        }

        public void Delete(string fileName)
        {
            var filePath = Path.Combine(new[] {_options.FileSaveAbsolutePath, fileName});

            if (File.Exists(filePath))
                File.Delete(filePath);
            else
                Log.Error($"File is not existing. Most likely \"{_options.FileSaveAbsolutePath}\" configuration parameter has typo.");
        }
    }
}
