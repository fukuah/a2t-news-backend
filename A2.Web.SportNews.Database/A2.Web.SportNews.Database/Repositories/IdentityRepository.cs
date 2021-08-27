using System;
using System.Threading.Tasks;
using A2.Web.SportNews.Database.Abstract;
using A2.Web.SportNews.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace A2.Web.SportNews.Database.Repositories
{
    public class IdentityRepository : IIdentityRepository
    {
        private readonly ApplicationDbContext _context;

        public IdentityRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<UserEntity> GetByUsernameAsync(string username)
            => _context.Users.FirstOrDefaultAsync(x => x.Username == username);
    }
}