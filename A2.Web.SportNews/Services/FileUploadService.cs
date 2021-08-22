using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace A2.Web.SportNews.Services
{
    public class FileUploadService
    {
        public const int MaxMbFileSize = 10;
        private static readonly string BaseFileSavePath = Path.Combine(new[]{"Resources", "Images"});
        private IWebHostEnvironment _hostingEnvironment;

        public FileUploadService(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment ?? throw new ArgumentNullException(nameof(hostingEnvironment));

            if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
            {
                _hostingEnvironment.WebRootPath = Directory.GetCurrentDirectory();
            }
        }

        public async Task Upload(string fileName, string fileB64)
        {
            var bytes = Convert.FromBase64String(fileB64);

            if (bytes.Length <= 0)
                throw new FileLoadException("File size is 0Mb.");
            if (bytes.Length > MaxMbFileSize * 1024 * 1024)
                throw new FileLoadException($"File is too large to load. The size restriction is {MaxMbFileSize}Mb.");

            var filePath = Path.Combine(new[] {_hostingEnvironment.WebRootPath, BaseFileSavePath, fileName});

            await File.WriteAllBytesAsync(filePath, bytes);
        }
    }
}
