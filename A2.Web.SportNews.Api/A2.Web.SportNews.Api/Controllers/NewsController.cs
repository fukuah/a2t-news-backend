using System;
using System.Threading.Tasks;
using A2.Web.SportNews.Api.Abstract;
using A2.Web.SportNews.Api.Mappers;
using A2.Web.SportNews.Api.Models.Common;
using A2.Web.SportNews.Api.Models.News;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A2.Web.SportNews.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController
    {
        private readonly INewsService _newsService;

        public NewsController(INewsService newsService)
        {
            _newsService = newsService ?? throw new ArgumentNullException(nameof(newsService));
        }

        [HttpPost]
        public async Task<PaginationModel<NewsPreviewModel>> GetPageAsync([FromBody] NewsPageRequestModel model)
        {
            var news = await _newsService.GetPageAsync(model.ToCore());

            return news.ToModel();
        }

        [HttpGet, Route("{id}")]
        public async Task<NewsArticleModel> GetArticle([FromRoute] int id)
        {
            return (await _newsService.GetByIdAsync(id)).ToArticleModel();
        }

        [Authorize]
        [HttpPost, Route("items")]
        public async Task<PaginationModel<NewsItemModel>> GetItemsPageAsync([FromBody] NewsPageRequestModel model)
        {
            var news = await _newsService.GetPageAsync(model.ToCore());

            return news.ToItemModel();
        }

        [Authorize]
        [HttpPost, Route("create"), DisableRequestSizeLimit]
        public async Task<ActionResult> CreateArticleAsync([FromBody] NewsCreateRequestModel model)
        {
            await _newsService.AddAsync(model.ToCore(), model.Image.ToCore());

            return new OkResult();
        }

        [Authorize]
        [HttpDelete, Route("{id}")]
        public async Task<ActionResult> DeleteArticleAsync([FromRoute] int id)
        {
            await _newsService.DeleteAsync(id);

            return new OkResult();
        }
    }
}
