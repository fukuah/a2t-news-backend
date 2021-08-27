using A2.Web.SportNews.Database.Entities;

namespace A2.Web.SportNews.Core.Mappers
{
    public static class NewsMapper
    {
        public static NewsCore ToCore(this NewsEntity entity)
        {
            if (entity == null) return null;

            return new NewsCore
            {
                Id = entity.Id,
                Content = entity.Content,
                CreatedTime = entity.CreatedTime,
                ImageLink = entity.ImageLink,
                PublishDate = entity.PublishDate,
                TextPreview = entity.TextPreview,
                Title = entity.Title
            };
        }

        public static NewsEntity ToEntity(this NewsCore core)
        {
            if (core == null) return null;

            return new NewsEntity
            {
                Content = core.Content,
                CreatedTime = core.CreatedTime,
                Id = core.Id,
                ImageLink = core.ImageLink,
                PublishDate = core.PublishDate,
                TextPreview = core.TextPreview,
                Title = core.Title
            };
        }
    }
}
