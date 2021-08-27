using System;
using System.Threading.Tasks;
using A2.Web.SportNews.Database.Entities;
using A2.Web.SportNews.Database.Repositories;

namespace A2.Web.SportNews.Database.Abstract
{
    public interface IUnitOfWork : IAsyncDisposable, IDisposable
    {
        IRepository<NewsEntity> NewsRepository { get; }
        IRepository<ContactPersonEntity> ContactsRepository { get; }
        IRepository<UserEntity> UsersRepository { get; }
        IIdentityRepository IdentityRepository{ get; }

        Task SaveChangesAsync();
    }
}
