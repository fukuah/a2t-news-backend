using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using A2.Web.SportNews.Models.Login;
using A2.Web.SportNews.Options;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace A2.Web.SportNews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly AuthOptions _authOptions;

        public AccountController(AuthOptions authOptions)
        {
            _authOptions = authOptions ?? throw new ArgumentNullException(nameof(authOptions));
        }

        // тестовые данные вместо использования базы данных
        private static readonly List<LoginRequestModel> _users = new List<LoginRequestModel>
        {
            new LoginRequestModel {Username="admin", Password="12345"},
            new LoginRequestModel { Username="qwerty", Password="55555"}
        };

        [HttpPost("/token")]
        public IActionResult Token([FromBody] LoginRequestModel model)
        {
            var identity = GetIdentity(model.Username, model.Password);
            if (identity == null)
            {
                return BadRequest(new { errorText = "Invalid username or password." });
            }

            var now = DateTime.UtcNow;
            // создаем JWT-токен
            var jwt = new JwtSecurityToken(
                    issuer: _authOptions.Issuer,
                    audience: _authOptions.Audience,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(_authOptions.Lifetime)),
                    signingCredentials: new SigningCredentials(_authOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                AccessToken = encodedJwt,
                Username = identity.Name,
                Lifetime = _authOptions.Lifetime
            };

            return Json(response);
        }

        private ClaimsIdentity GetIdentity(string username, string password)
        {
            LoginRequestModel person = _users.FirstOrDefault(x => x.Username == username && x.Password == password);
            if (person != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, person.Username),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin")
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // если пользователя не найдено
            return null;
        }
    }
}
