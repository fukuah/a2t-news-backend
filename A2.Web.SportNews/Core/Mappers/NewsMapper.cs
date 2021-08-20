using System;
using System.Linq;
using A2.Web.SportNews.Entities;
using A2.Web.SportNews.Models.Common;
using A2.Web.SportNews.Models.News;

namespace A2.Web.SportNews.Core.Mappers
{
    public static class NewsMapper
    {
        public static PaginationModel<NewsPreviewModel> ToModel(this Pagination<NewsCore> cores)
        {
            return new PaginationModel<NewsPreviewModel>
            {
                Count = cores.Count,
                Limit = cores.Limit,
                Offset = cores.Offset,
                Models = cores.Items.Select(x => x.ToModel()).ToList()
            };
        }

        public static NewsPreviewModel ToModel(this NewsCore core)
        {
            return new NewsPreviewModel
            {
                Id = core.Id,
                ImageLink = core.ImageLink,
                PublishDate = core.PublishDate,
                TextPreview = core.TextPreview,
                Title = core.Title
            };
        }

        public static NewsArticleModel ToArticleModel(this NewsCore core)
        {
            return new NewsArticleModel
            {
                Content = core.Content,
                ImageLink = core.ImageLink,
                Id = core.Id,
                PublishDate = core.PublishDate,
                Title = core.Title
            };
        }

        public static NewsCore ToCore(this NewsEntity entity)
        {
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

        public static NewsCore ToCore(this NewsCreateRequestModel model)
        {
            return new NewsCore
            {
                Content = model.Content,
                PublishDate = model.PublishDate ?? DateTime.UtcNow,
                CreatedTime = model.PublishDate ?? DateTime.UtcNow,
                TextPreview = model.TextPreview,
                Title = model.Title
            };
        }

        public static NewsEntity ToEntity(this NewsCore core)
        {
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
