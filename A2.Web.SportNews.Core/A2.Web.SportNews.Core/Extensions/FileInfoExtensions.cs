using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2.Web.SportNews.Core.Extensions
{
    public static class FileInfoExtensions
    {
        public static bool HasFile(this FileInfoCore fileInfo) 
            => fileInfo != null && !string.IsNullOrWhiteSpace(fileInfo.Name) && !string.IsNullOrWhiteSpace(fileInfo.FileB64);
    }
}
