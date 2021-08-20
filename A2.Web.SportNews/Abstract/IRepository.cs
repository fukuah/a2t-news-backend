using System.Collections.Generic;
using System.Threading.Tasks;
using A2.Web.SportNews.Entities;

namespace A2.Web.SportNews.Abstract
{
    public interface IRepository<TEntity>
    {
        Task<ICollection<TEntity>> GetEntities(int? limit, int? offset);
        Task<TEntity> GetEntity(int id);
        Task<NewsEntity> Update(NewsEntity entity);
        void Add(NewsEntity entity);
        void Delete(int id);
        Task<int> Count();
    }
}
