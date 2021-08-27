using System;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using A2.Web.SportNews.Api.Mappers;
using A2.Web.SportNews.Api.Models.Login;
using A2.Web.SportNews.Auth;
using A2.Web.SportNews.Auth.Abstract;
using A2.Web.SportNews.Options;
using Microsoft.AspNetCore.Mvc;

namespace A2.Web.SportNews.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private readonly AuthOptions _authOptions;
        private readonly IIdentityService _identityService;
        private readonly IUserRightsService _userRightsService;
        private readonly JwtTokenBuilder _jwtBuilder;

        public AccountController(
            AuthOptions authOptions, 
            IIdentityService identityService, 
            IUserRightsService userRightsService, 
            JwtTokenBuilder jwtBuilder)
        {
            _authOptions = authOptions ?? throw new ArgumentNullException(nameof(authOptions));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _userRightsService = userRightsService ?? throw new ArgumentNullException(nameof(userRightsService));
            _jwtBuilder = jwtBuilder ?? throw new ArgumentNullException(nameof(jwtBuilder));
        }

        [HttpPost("/token")]
        public async Task<IActionResult> GetTokenAsync([FromBody] LoginRequestModel model)
        {
            var identity = await _identityService.GetIdentityAsync(model.ToRequest());
            var userRights = _userRightsService.GetRights(identity);

            if (userRights == null)
            {
                return Unauthorized(new { errorText = "Invalid username or password." });
            }

            var jwt = _jwtBuilder.Build(userRights);
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            var response = new
            {
                AccessToken = encodedJwt,
                Username = userRights.Name,
                _authOptions.Lifetime
            };

            return Json(response);
        }
    }
}
