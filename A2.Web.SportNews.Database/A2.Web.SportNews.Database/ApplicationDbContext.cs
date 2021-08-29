using System;
using System.Security.Cryptography;
using A2.Web.SportNews.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace A2.Web.SportNews.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<ContactPersonEntity> ContactPersons { get; set; }
        public DbSet<NewsEntity> News { get; set; }
        public DbSet<UserEntity> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }

        #region PasswordHasher dublicate, to generate user password hashes

        private const int SaltSize = 16; // 128 bit 
        private const int KeySize = 32; // 256 bit

        private int Iterations { get; } = 1000;
        private string Hash(string password)
        {
            using var algorithm = new Rfc2898DeriveBytes(
                password,
                SaltSize,
                Iterations,
                HashAlgorithmName.SHA256);
            var key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
            var salt = Convert.ToBase64String(algorithm.Salt);

            return $"{Iterations}.{salt}.{key}";
        }

        #endregion
    }
}
