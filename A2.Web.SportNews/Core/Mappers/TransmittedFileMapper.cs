using System;
using System.IO;
using A2.Web.SportNews.Models.Common;

namespace A2.Web.SportNews.Core.Mappers
{
    public static class TransmittedFileMapper
    {
        public static FileInfoCore ToCore(this FileInfoModel infoModel)
        {
            if (infoModel == null) return null;

            var fileFormat = infoModel.Format != TransmittedFileFormat.Unknown
                ? "." + infoModel.Format.ToString().ToLowerInvariant()
                : string.Empty;
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