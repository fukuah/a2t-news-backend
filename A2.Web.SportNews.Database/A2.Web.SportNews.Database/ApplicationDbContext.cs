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
            //modelBuilder.Entity<NewsEntity>().HasData(
            //    new NewsEntity
            //    {
            //        Id = 13,
            //        Content = "2 AAAAAAAAAAAAA!!!!",
            //        CreatedTime = DateTime.UtcNow,
            //        PublishDate = DateTime.UtcNow,
            //        ImageLink = "",
            //        TextPreview = "2 A A A A A A 222",
            //        Title = "2 МЕССИ ЗАБИЛ СЕБЕ В СРАКУ!!!"
            //    },
            //    new NewsEntity
            //    {
            //        Id = 4,
            //        Content = "2 УУУУУУУУУУУУУУУУУУУ!!!!",
            //        CreatedTime = DateTime.UtcNow,
            //        PublishDate = DateTime.UtcNow,
            //        ImageLink = "",
            //        TextPreview = "Б Б Б Б Б 211112",
            //        Title = "2 МЕССИ ЗАБИЛ СЕБЕ В СРАКУ ДВА МЯЧА!!!"
            //    },
            //    new NewsEntity
            //    {
            //        Id = 19,
            //        Content = "ЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕ!!!!",
            //        CreatedTime = DateTime.UtcNow,
            //        PublishDate = DateTime.UtcNow,
            //        ImageLink = "",
            //        TextPreview = "Ниче се",
            //        Title = "МЕССИ ЗАБИЛ ГОЛА!!!"
            //    });


            //modelBuilder.Entity<UserEntity>().HasData(
            //        new []
            //        {
            //            new UserEntity
            //            {
            //                Id = 1,
            //                Username = "qwerty",
            //                PasswordHash = Hash("qwerty")
            //            },
            //            new UserEntity
            //            {
            //                Id = 2,
            //                Username = "qwerty1",
            //                PasswordHash = Hash("qwerty1")
            //            },
            //            new UserEntity
            //            {
            //                Id = 3,
            //                Username = "qwerty2",
            //                PasswordHash = Hash("qwerty2")
            //            }
            //        }
            //    );
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
