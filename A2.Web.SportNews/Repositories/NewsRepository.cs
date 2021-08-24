using System.Linq;
using A2.Web.SportNews.Database;
using A2.Web.SportNews.Entities;

namespace A2.Web.SportNews.Repositories
{
    public class NewsRepository : Repository<NewsEntity>
    {
        public NewsRepository(ApiContext context) : base(context)
        {
        }

        protected override IQueryable<NewsEntity> ApplySort(IQueryable<NewsEntity> query) => query.OrderByDescending(x => x.PublishDate);
        
    }
}
