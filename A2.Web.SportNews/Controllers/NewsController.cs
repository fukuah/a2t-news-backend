using System;
using System.IO;
using System.Threading.Tasks;
using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Core.Mappers;
using A2.Web.SportNews.Models.Common;
using A2.Web.SportNews.Models.News;
using A2.Web.SportNews.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A2.Web.SportNews.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController
    {
        private readonly INewsService _newsService;
        private readonly FileUploadService _fileUploadService;

        public NewsController(INewsService newsService, FileUploadService fileUploadService)
        {
            _newsService = newsService ?? throw new ArgumentNullException(nameof(newsService));
            _fileUploadService = fileUploadService ?? throw new ArgumentNullException(nameof(fileUploadService));
        }

        [HttpPost]
        public async Task<PaginationModel<NewsPreviewModel>> GetPageAsync([FromBody] NewsPageRequestModel model)
        {
            var news = await _newsService.GetNewsPageAsync(model.ToCore());

            return news.ToModel();
        }

        [HttpGet, Route("{id}")]
        public async Task<NewsArticleModel> GetArticle([FromRoute] int id)
        {
            return (await _newsService.GetNewsByIdAsync(id)).ToArticleModel();
        }

        [Authorize]
        [HttpPost, Route("items")]
        public async Task<PaginationModel<NewsItemModel>> GetItemsPageAsync([FromBody] NewsPageRequestModel model)
        {
            var news = await _newsService.GetNewsPageAsync(model.ToCore());

            return news.ToItemModel();
        }

        [Authorize]
        [HttpPost, Route("create"), DisableRequestSizeLimit]
        public ActionResult CreateArticle([FromBody] NewsCreateRequestModel model)
        {
            _newsService.AddArticle(model.ToCore(), model.ImageFile);

            return new OkResult();
        }

        [Authorize]
        [HttpDelete, Route("{id}")]
        public ActionResult DeleteArticle([FromRoute] int id)
        {
            _newsService.DeleteArticle(id);

            return new OkResult();
        }
    }
}
