using System.Collections.Generic;
using System.Threading.Tasks;
using A2.Web.SportNews.Database.Entities;

namespace A2.Web.SportNews.Database.Abstract
{
    public interface IRepository<TEntity>
        where TEntity : Entity
    {
        Task<List<TEntity>> GetEntitiesAsync(int? limit = null, int? offset = null);
        Task<TEntity> GetEntityAsync(int id);
        Task UpdateAsync(TEntity entity);
        Task AddAsync(TEntity entity);
        Task DeleteAsync(int id);
        Task<int> CountAsync();
    }
}
