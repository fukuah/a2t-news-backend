using Microsoft.Extensions.Options;

namespace A2.Web.SportNews.Options
{
    public sealed class CorsPolicyOptions : IOptions<CorsPolicyOptions>
    {
        public const string SectionName = "CorsPolicy";
        public string Key { get; set; }
        public string[] AllowedHosts { get; set; }
        public CorsPolicyOptions Value => this;
    }
}