using System;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace A2.Web.SportNews.Options
{
    public class ApiOptions : IOptions<ApiOptions>
    {
        public const string Section = "Api";
        public string FileSavePath { get; set; }
        public ApiOptions Value => new ApiOptions
        {
            FileSavePath = "Resources/Images"
        };
    }

    public class AuthOptions : IOptions<AuthOptions>
    {
        public const string Section = "Auth";
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Key { get; set; }
        public Int32 Lifetime { get; set; }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
        public AuthOptions Value => new AuthOptions
        {
            Key = "k",
            Issuer = "",
            Audience = "",
            Lifetime = 300
        };
    }
}