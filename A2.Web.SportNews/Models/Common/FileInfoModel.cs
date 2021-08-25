using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2.Web.SportNews.Models.Common
{
    public enum TransmittedFileFormat
    {
        Unknown,
        Png,
        Jpg
    }

    public class FileInfoModel
    {
        public string FileName { get; set; }
        public TransmittedFileFormat Format { get; set; }
        public string FileB64 { get; set; }
    }
}
