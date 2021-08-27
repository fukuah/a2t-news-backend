using System.Linq;
using A2.Web.SportNews.Database.Abstract;
using A2.Web.SportNews.Database.Entities;

namespace A2.Web.SportNews.Database.Repositories
{
    public class UsersRepository : Repository<UserEntity>
    {
        public UsersRepository(ApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<UserEntity> ApplySort(IQueryable<UserEntity> query) => query;
    }
}
