using System;
using Microsoft.Extensions.Options;

namespace A2.Web.SportNews.Options
{
    public sealed class AuthOptions : IOptions<AuthOptions>
    {
        public const string SectionName = "Auth";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public Int32 Lifetime { get; set; }

        public AuthOptions Value => this;
    }
}