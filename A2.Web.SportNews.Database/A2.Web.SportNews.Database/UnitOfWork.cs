using System;
using System.Threading.Tasks;
using A2.Web.SportNews.Database.Abstract;
using A2.Web.SportNews.Database.Entities;
using A2.Web.SportNews.Database.Repositories;

namespace A2.Web.SportNews.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IRepository<NewsEntity> NewsRepository { get; }
        public IRepository<ContactPersonEntity> ContactsRepository { get; }
        public IRepository<UserEntity> UsersRepository { get; }
        public IIdentityRepository IdentityRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            NewsRepository = new NewsRepository(context);
            ContactsRepository = new ContactPersonRepository(context);
            UsersRepository = new UsersRepository(context);
            IdentityRepository = new IdentityRepository(context);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public  ValueTask DisposeAsync()
        {
            return _context.DisposeAsync();
        }
    }
}
