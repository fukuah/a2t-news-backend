using System.Linq;
using A2.Web.SportNews.Database.Abstract;
using A2.Web.SportNews.Database.Entities;

namespace A2.Web.SportNews.Database.Repositories
{
    public class ContactPersonRepository : Repository<ContactPersonEntity>
    {
        public ContactPersonRepository(ApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<ContactPersonEntity> ApplySort(IQueryable<ContactPersonEntity> query) => query.OrderByDescending(x => x.Id);
    }
}
