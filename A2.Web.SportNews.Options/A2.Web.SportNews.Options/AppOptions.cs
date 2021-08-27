using System.Collections.Generic;
using Microsoft.Extensions.Options;

namespace A2.Web.SportNews.Options
{
    public sealed class AppOptions : IOptions<AppOptions>
    {
        public const string SectionName = "App";
        public int HashIterations { get; set; } = 1000;
        public string FileSaveAbsolutePath { get; set; }
        public bool UseInMemoryDb { get; set; }
        public string Connection { get; set; }
        public AppOptions Value => this;
    }
}