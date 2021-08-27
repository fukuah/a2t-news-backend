using System.Threading.Tasks;
using A2.Web.SportNews.Database.Entities;

namespace A2.Web.SportNews.Database.Abstract
{
    public interface IIdentityRepository
    {
        Task<UserEntity> GetByUsernameAsync(string username);
    }
}
