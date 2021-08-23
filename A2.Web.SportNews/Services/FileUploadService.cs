using System;
using System.IO;
using System.Threading.Tasks;
using A2.Web.SportNews.Options;


namespace A2.Web.SportNews.Services
{
    public class FileUploadService
    {
        public const int MaxMbFileSize = 10;
        private readonly ApiOptions _options;

        public FileUploadService(ApiOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public async Task Upload(string fileName, string fileB64)
        {
            var bytes = Convert.FromBase64String(fileB64);

            if (bytes.Length <= 0)
                throw new FileLoadException("File size is 0Mb.");
            if (bytes.Length > MaxMbFileSize * 1024 * 1024)
                throw new FileLoadException($"File is too large to load. The size restriction is {MaxMbFileSize}Mb.");

            var filePath = Path.Combine(new[] {_options.FileSavePath, fileName});

            await File.WriteAllBytesAsync(filePath, bytes);
        }
    }
}
