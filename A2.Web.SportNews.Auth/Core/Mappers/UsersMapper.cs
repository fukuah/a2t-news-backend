using A2.Web.SportNews.Auth.Core.Requests;
using A2.Web.SportNews.Database.Entities;

namespace A2.Web.SportNews.Auth.Core.Mappers
{
    public static class UsersMapper
    {
        public static UserCore ToCore(this UserEntity entity)
        {
            if (entity == null) return null;

            return new UserCore
            {
                Id = entity.Id,
                PasswordHash = entity.PasswordHash,
                Username = entity.Username
            };
        }
    }
}
