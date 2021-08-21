using System.Collections.Generic;
using System.Threading.Tasks;
using A2.Web.SportNews.Entities;

namespace A2.Web.SportNews.Abstract
{
    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        Task<ICollection<TEntity>> GetEntities(int? limit = null, int? offset = null);
        Task<TEntity> GetEntity(int id);
        void Update(TEntity entity);
        void Add(TEntity entity);
        void Delete(int id);
        int Count();
    }
}
