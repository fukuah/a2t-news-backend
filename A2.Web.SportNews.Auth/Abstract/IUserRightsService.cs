using System.Security.Claims;
using A2.Web.SportNews.Auth.Core;

namespace A2.Web.SportNews.Auth.Abstract
{
    public interface IUserRightsService
    {
        ClaimsIdentity GetRights(UserCore request);
    }
}