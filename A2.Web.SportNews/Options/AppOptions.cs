using Microsoft.Extensions.Options;

namespace A2.Web.SportNews.Options
{
    public class AppOptions : IOptions<AppOptions>
    {
        public const string SectionName = "App";
        public string FileSaveAbsolutePath { get; set; }
        public AppOptions Value => this;
    }
}