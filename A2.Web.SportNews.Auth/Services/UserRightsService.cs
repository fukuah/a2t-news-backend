using System.Collections.Generic;
using System.Security.Claims;
using A2.Web.SportNews.Auth.Abstract;
using A2.Web.SportNews.Auth.Core;

namespace A2.Web.SportNews.Auth.Services
{
    public class UserRightsService : IUserRightsService
    {
        public ClaimsIdentity GetRights(UserCore user)
        {
            if (user == null) return null;

            // Just for now there are no other users than admin
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Username),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, UserRoles.Admin.RoleName)
            };
            var claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
    }
}