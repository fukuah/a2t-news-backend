using System;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace A2.Web.SportNews.Options
{
    public class AuthOptions : IOptions<AuthOptions>
    {
        public const string SectionName = "Auth";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public Int32 Lifetime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }

        public AuthOptions Value => this;
    }
}