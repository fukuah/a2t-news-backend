using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using A2.Web.SportNews.Options;
using Microsoft.IdentityModel.Tokens;

namespace A2.Web.SportNews.Auth
{
    public class JwtTokenBuilder
    {
        private readonly AuthOptions _options;

        public JwtTokenBuilder(AuthOptions options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }
        public JwtSecurityToken Build(ClaimsIdentity userRights)
        {
            var now = DateTime.UtcNow;
            return new JwtSecurityToken(
                issuer: _options.Issuer,
                audience: _options.Audience,
                notBefore: now,
                claims: userRights.Claims,
                expires: now.Add(TimeSpan.FromMinutes(_options.Lifetime)),
                signingCredentials: new SigningCredentials(GetSymmetricSecurityKey(_options.Key), SecurityAlgorithms.HmacSha256));
        }

        public static SymmetricSecurityKey GetSymmetricSecurityKey(string key)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key));
        }
    }
}
