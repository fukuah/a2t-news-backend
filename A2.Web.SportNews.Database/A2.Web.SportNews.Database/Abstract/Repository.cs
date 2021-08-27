using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using A2.Web.SportNews.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace A2.Web.SportNews.Database.Abstract
{
    public abstract class Repository<TEntity> : IRepository<TEntity>
        where TEntity : Entity
    {
        private readonly ApplicationDbContext _context;

        protected Repository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        protected abstract IQueryable<TEntity> ApplySort(IQueryable<TEntity> query);

        public Task<List<TEntity>> GetEntitiesAsync(int? limit, int? offset = null)
        {
            // TODO add cancellation token
            var query = _context.Set<TEntity>().Select(x => x);

            query = ApplySort(query);

            if (limit.HasValue && offset.HasValue)
            {
                return query
                    .Skip(offset.Value)
                    .Take(limit.Value)
                    .ToListAsync(CancellationToken.None);
            }
            if (offset.HasValue)
            {
                return query
                    .Skip(offset.Value)
                    .ToListAsync(CancellationToken.None);
            }

            return query.ToListAsync(CancellationToken.None);
        }

        public Task<TEntity> GetEntityAsync(int id) 
            => _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);

        public async Task UpdateAsync(TEntity updated)
        {
            if (updated == null)
                throw new ArgumentNullException($"An attempt to update record of {typeof(TEntity)} type with null");

            var entityToUpdate = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == updated.Id);
            if (entityToUpdate == null)
                throw new KeyNotFoundException(
                    $"An attempt to update record of {typeof(TEntity)} type which is not exists. Id={updated.Id}");

            // Set attachment not to be updated in case nothing has been sent
            if (entityToUpdate is EntityWithAttachment entityWithAttachment)
            {
                var entry = _context.Entry(entityWithAttachment);
                entry.CurrentValues.SetValues(updated);
                if (string.IsNullOrWhiteSpace(entityWithAttachment.ImageLink))
                    entry.Property(x => x.ImageLink).IsModified = false;
            }
            else
            {
                _context.Entry(entityToUpdate).CurrentValues.SetValues(updated);
            }
        }

        public async Task AddAsync(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentException(nameof(entity));

            await _context.Set<TEntity>().AddAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<TEntity>().FirstOrDefaultAsync(x => x.Id == id);
            if (entity == null) return;

            _context.Remove(entity);
        }

        public Task<int> CountAsync()
        {
            return _context.Set<NewsEntity>().CountAsync();
        }
    }
}
