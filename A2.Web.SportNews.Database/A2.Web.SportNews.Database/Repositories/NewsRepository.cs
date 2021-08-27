using System.Linq;
using A2.Web.SportNews.Database.Abstract;
using A2.Web.SportNews.Database.Entities;

namespace A2.Web.SportNews.Database.Repositories
{
    public class NewsRepository : Repository<NewsEntity>
    {
        public NewsRepository(ApplicationDbContext context) : base(context)
        {
        }

        protected override IQueryable<NewsEntity> ApplySort(IQueryable<NewsEntity> query) => query.OrderByDescending(x => x.PublishDate);
        
    }
}
