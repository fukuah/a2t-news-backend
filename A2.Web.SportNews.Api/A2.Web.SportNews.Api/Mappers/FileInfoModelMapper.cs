using System.IO;
using A2.Web.SportNews.Api.Models.Common;
using A2.Web.SportNews.Core;
using Serilog;

namespace A2.Web.SportNews.Api.Mappers
{
    public static class FileInfoModelMapper
    {
        public static FileInfoCore ToCore(this FileInfoModel infoModel)
        {
            if (infoModel == null) return null;

            if (infoModel.Format == TransmittedFileFormat.Unknown)
            {
                Log.Warning("File of unknown format has been delivered.");
                return null;
            }

            var fileFormat = "." + infoModel.Format.ToString().ToLowerInvariant();
                
            var fileName = string.IsNullOrWhiteSpace(infoModel.FileName) 
                ? Path.GetRandomFileName() 
                : infoModel.FileName;
            return new FileInfoCore
            {
                Name = fileName + fileFormat,
                FileB64 = infoModel.FileB64
            };
        }
    }
}