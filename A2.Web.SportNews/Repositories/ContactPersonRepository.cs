using System.Linq;
using A2.Web.SportNews.Database;
using A2.Web.SportNews.Entities;

namespace A2.Web.SportNews.Repositories
{
    public class ContactPersonRepository : Repository<ContactPersonEntity>
    {
        public ContactPersonRepository(ApiContext context) : base(context)
        {
        }

        protected override IQueryable<ContactPersonEntity> ApplySort(IQueryable<ContactPersonEntity> query) => query.OrderByDescending(x => x.Id);
    }
}
