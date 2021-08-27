using System.Threading.Tasks;
using A2.Web.SportNews.Auth.Core;
using A2.Web.SportNews.Auth.Core.Requests;

namespace A2.Web.SportNews.Auth.Abstract
{
    public interface IIdentityService
    {
        Task<UserCore> GetIdentityAsync(LoginRequest request);
    }
}