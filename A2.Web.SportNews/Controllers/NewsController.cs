using System;
using System.Threading.Tasks;
using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Core.Mappers;
using A2.Web.SportNews.Models.Common;
using A2.Web.SportNews.Models.News;
using Microsoft.AspNetCore.Mvc;

namespace A2.Web.SportNews.Controllers
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
            var news = await _newsService.GetNewsPageAsync(model.ToCore());

            return news.ToModel();
        }

        [HttpGet, Route("{id}")]
        public async Task<NewsArticleModel> GetArticle([FromRoute] int id)
        {
            return (await _newsService.GetNewsByIdAsync(id)).ToArticleModel();
        }

        [HttpPost, Route("create")]
        public ActionResult CreateArticle([FromBody] NewsCreateRequestModel model)
        {
            _newsService.AddArticle(model.ToCore());

            return new OkResult();
        }

        [HttpDelete, Route("{id}")]
        public ActionResult DeleteArticle([FromRoute] int id)
        {
            _newsService.DeleteArticle(id);

            return new OkResult();
        }
    }
}
