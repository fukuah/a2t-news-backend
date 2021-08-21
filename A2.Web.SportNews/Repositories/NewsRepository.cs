using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Database;
using A2.Web.SportNews.Entities;
using Microsoft.EntityFrameworkCore;

namespace A2.Web.SportNews.Repositories
{
    public class NewsRepository : IRepository<NewsEntity>
    {
        private ApiContext _context;

        public NewsRepository(ApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<ICollection<NewsEntity>> GetEntities(int? limit, int? offset)
        {
            var query = _context.News
                .Select(x => x)
                .OrderByDescending(x => x.PublishDate);

            if (limit.HasValue && offset.HasValue)
            {
                return await query
                    .Skip(offset.Value)
                    .Take(limit.Value)
                    .ToListAsync(CancellationToken.None);
            }
            if (offset.HasValue)
            {
                return await query
                    .Skip(offset.Value)
                    .ToListAsync(CancellationToken.None);
            }

            return await query.ToListAsync(CancellationToken.None);
        }

        public Task<NewsEntity> GetEntity(int id)
        {
            return _context.News.FirstOrDefaultAsync(x => x.Id == id);
        }

        public Task<NewsEntity> Update(NewsEntity entity)
        {
            //var entityToUpdate = _context.News.FirstOrDefault(x => x.Id == entity.Id);
            //if (entityToUpdate == null) return null;
            
            //entityToUpdate.Content = entity.Content;
            //entityToUpdate.ImageLink = entity.ImageLink;
            //entityToUpdate.PublishDate = entity.PublishDate;
            //entityToUpdate.TextPreview = entity.TextPreview;
            //entityToUpdate.Title = entity.Title;

            //_context.News.

            //return Task.FromResult(entityToUpdate);
            throw new NotImplementedException();
        }

        public void Add(NewsEntity entity)
        {
            _context.News.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.News.FirstOrDefault(x => x.Id == id);
            if (entity == null) return;

            _context.Remove(entity);
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.News.Count();
        }
    }
}
