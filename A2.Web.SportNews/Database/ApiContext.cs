using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Entities;
using Microsoft.EntityFrameworkCore;

namespace A2.Web.SportNews.Database
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> options)
            : base(options)
        {
        }

        public DbSet<ContactPersonEntity> ContactPersons { get; set; }

        public DbSet<NewsEntity> News { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<NewsEntity>().HasData(
                new NewsEntity
                {
                    Id = 13,
                    Content = "2 AAAAAAAAAAAAA!!!!",
                    CreatedTime = DateTime.UtcNow,
                    PublishDate = DateTime.UtcNow,
                    ImageLink = "",
                    TextPreview = "2 A A A A A A 222",
                    Title = "2 МЕССИ ЗАБИЛ СЕБЕ В СРАКУ!!!"
                },
                new NewsEntity
                {
                    Id = 4,
                    Content = "2 УУУУУУУУУУУУУУУУУУУ!!!!",
                    CreatedTime = DateTime.UtcNow,
                    PublishDate = DateTime.UtcNow,
                    ImageLink = "",
                    TextPreview = "Б Б Б Б Б 211112",
                    Title = "2 МЕССИ ЗАБИЛ СЕБЕ В СРАКУ ДВА МЯЧА!!!"
                },
                new NewsEntity
                {
                    Id = 19,
                    Content = "ЕЕЕЕЕЕЕЕЕЕЕЕЕЕЕ!!!!",
                    CreatedTime = DateTime.UtcNow,
                    PublishDate = DateTime.UtcNow,
                    ImageLink = "",
                    TextPreview = "Ниче се",
                    Title = "МЕССИ ЗАБИЛ ГОЛА!!!"
                });
        }
    }
}
