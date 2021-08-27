using System.Threading.Tasks;
using A2.Web.SportNews.Database.Entities;

namespace A2.Web.SportNews.Api.Abstract
{
    public interface IUserService
    {
        Task<UserEntity> GetByUsernameAsync(string username);
    }
}
