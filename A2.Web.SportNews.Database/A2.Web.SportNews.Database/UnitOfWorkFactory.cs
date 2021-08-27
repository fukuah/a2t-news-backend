using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Database.Abstract;
using Microsoft.EntityFrameworkCore;

namespace A2.Web.SportNews.Database
{
    public class UnitOfWorkFactory
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public UnitOfWorkFactory(DbContextOptions<ApplicationDbContext> options)
        {
            _options = options ?? throw new ArgumentNullException(nameof(options));
        }

        public IUnitOfWork Create()
        {
            return new UnitOfWork(new ApplicationDbContext(_options));
        }
    }
}
