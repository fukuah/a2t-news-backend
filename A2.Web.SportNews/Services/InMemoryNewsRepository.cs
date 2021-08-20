using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Entities;

namespace A2.Web.SportNews.Services
{
    public class InMemoryNewsRepository : IRepository<NewsEntity>
    {
        public InMemoryNewsRepository()
        {
            _idIterator = 0;
            _news = new ConcurrentDictionary<NewsEntity, object>();
        }

        private readonly ConcurrentDictionary<NewsEntity, object> _news;
        private volatile int _idIterator;

        public Task<ICollection<NewsEntity>> GetEntities(int? limit, int? offset)
        {
            var query = _news
                .Select(x => x.Key)
                .OrderByDescending(x => x.PublishDate);

            if (limit.HasValue && offset.HasValue)
            {
                return Task.FromResult<ICollection<NewsEntity>>(query
                    .Skip(offset.Value)
                    .Take(limit.Value)
                    .ToList());
            }
            if (offset.HasValue)
            {
                return Task.FromResult<ICollection<NewsEntity>>(query
                    .Skip(offset.Value)
                    .ToList());
            }

            return Task.FromResult<ICollection<NewsEntity>>(query.ToList());
        }

        public Task<NewsEntity> GetEntity(int id)
        {
            return Task.FromResult(_news.Select(x => x.Key).FirstOrDefault(x => x.Id == id));
        }

        public Task<NewsEntity> Update(NewsEntity entity)
        {
            var entityToUpdate = _news.Select(x => x.Key).FirstOrDefault(x => x.Id == entity.Id);
            
            if (entityToUpdate == null) return null;

            entityToUpdate.Content = entity.Content;
            entityToUpdate.ImageLink = entity.ImageLink;
            entityToUpdate.PublishDate = entity.PublishDate;
            entityToUpdate.TextPreview = entity.TextPreview;
            entityToUpdate.Title = entity.Title;

            _news.AddOrUpdate(entityToUpdate, default!, (newsEntity, o) => null);

            return Task.FromResult(entityToUpdate);
        }

        public void Add(NewsEntity entity)
        {
            entity.Id = _idIterator + 1;
            _news.AddOrUpdate(entity, default!, (newsEntity, o) => null);
        }

        public void Delete(int id)
        {
            _news.Remove(_news.Select(x => x.Key).FirstOrDefault(x => x.Id == id), out _);
        }

        public Task<int> Count()
        {
            return Task.FromResult(_news.Count);
        }
    }
}
