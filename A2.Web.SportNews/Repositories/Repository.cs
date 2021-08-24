using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Database;
using A2.Web.SportNews.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace A2.Web.SportNews.Repositories
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : EntityWithImage
    {
        private readonly ApiContext _context;

        protected Repository(ApiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected abstract IQueryable<TEntity> ApplySort(IQueryable<TEntity> query);

        public async Task<ICollection<TEntity>> GetEntities(int? limit, int? offset)
        {
            // TODO add cancellation token
            var query = _context.Set<TEntity>().Select(x => x);

            query = ApplySort(query);

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

        public Task<TEntity> GetEntity(int id)
        {
            return _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public void Update(TEntity updated)
        {
            // TODO Add exceptions & handling
            if (updated == null)
                return;

            // TODO Add exceptions & handling
            var entityToUpdate = _context.Set<TEntity>().FirstOrDefault(x => x.Id == updated.Id);
            if (entityToUpdate == null) return;

            var entry = _context.Entry(entityToUpdate);
            entry.CurrentValues.SetValues(updated);
            if (string.IsNullOrWhiteSpace(entityToUpdate.ImageLink))
                entry.Property(x => x.ImageLink).IsModified = false;
            _context.SaveChanges();
        }

        public int Add(TEntity entity)
        {
            // TODO Add exceptions & handling
            if (entity == null)
                throw new ArgumentException(nameof(entity));

            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
            return entity.Id;
        }

        public void Delete(int id)
        {
            var entity = _context.Set<TEntity>().FirstOrDefault(x => x.Id == id);
            // TODO Add exceptions & handling
            if (entity == null) return;

            _context.Remove(entity);
            _context.SaveChanges();
        }

        public int Count()
        {
            return _context.Set<NewsEntity>().Count();
        }
    }
}
