using System;
using System.Linq;
using System.Text;
using A2.Web.SportNews.Api.Models.Common;
using A2.Web.SportNews.Api.Models.News;
using A2.Web.SportNews.Core;

namespace A2.Web.SportNews.Api.Mappers
{
    public static class NewsModelMapper
    {
        public static PaginationModel<NewsPreviewModel> ToModel(this Pagination<NewsCore> cores)
        {
            if (cores == null) return null;

            return new PaginationModel<NewsPreviewModel>
            {
                Count = cores.Count,
                Limit = cores.Limit,
                Offset = cores.Offset,
                Models = cores.Items.Select(x => x.ToModel()).ToList()
            };
        }

        public static PaginationModel<NewsItemModel> ToItemModel(this Pagination<NewsCore> cores)
        {
            if (cores == null) return null;

            return new PaginationModel<NewsItemModel>
            {
                Count = cores.Count,
                Limit = cores.Limit,
                Offset = cores.Offset,
                Models = cores.Items.Select(x => x.ToItemModel()).ToList()
            };
        }

        public static NewsPreviewModel ToModel(this NewsCore core)
        {
            if (core == null) return null;

            return new NewsPreviewModel
            {
                Id = core.Id,
                ImageLink = core.ImageLink,
                PublishDate = core.PublishDate,
                TextPreview = core.TextPreview,
                Title = core.Title
            };
        }

        public static NewsItemModel ToItemModel(this NewsCore core)
        {
            if (core == null) return null;

            return new NewsItemModel
            {
                Id = core.Id,
                PublishDate = core.PublishDate,
                Title = core.Title
            };
        }

        public static NewsArticleModel ToArticleModel(this NewsCore core)
        {
            if (core == null) return null;

            return new NewsArticleModel
            {
                Content = core.Content,
                ImageLink = core.ImageLink,
                Id = core.Id,
                PublishDate = core.PublishDate,
                Title = core.Title
            };
        }

        public static NewsCore ToCore(this NewsCreateRequestModel model)
        {
            if (model == null) return null;

            return new NewsCore
            {
                Content = model.Content,
                PublishDate = model.PublishDate ?? DateTime.UtcNow,
                CreatedTime = model.PublishDate ?? DateTime.UtcNow,
                TextPreview = model.TextPreview,
                Title = model.Title
            };
        }
    }
}
