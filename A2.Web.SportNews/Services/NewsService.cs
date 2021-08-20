using System;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Core;
using A2.Web.SportNews.Core.Mappers;
using A2.Web.SportNews.Core.Requests;
using A2.Web.SportNews.Entities;

namespace A2.Web.SportNews.Services
{
    public class NewsService : INewsService
    {
        private readonly IRepository<NewsEntity> _newsRepository;

        public NewsService(IRepository<NewsEntity> newsRepository)
        {
            _newsRepository = newsRepository ?? throw new ArgumentNullException(nameof(newsRepository));
        }

        public async Task<Pagination<NewsCore>> GetNewsPageAsync(NewsPageRequest request)
        {
            var news = await _newsRepository.GetEntities(request.Limit, request.Offset);

            return new Pagination<NewsCore>
            {
                Count = await _newsRepository.Count(),
                Offset = request.Offset,
                Limit = request.Limit,
                Items = news.Select(x => x.ToCore()).ToList()
            };
        }

        public async Task<NewsCore> GetNewsByIdAsync(int id) => (await _newsRepository.GetEntity(id)).ToCore();
        public void AddArticle(NewsCore article)
        {
            _newsRepository.Add(article.ToEntity());
        }

        public void DeleteArticle(int id)
        {
            _newsRepository.Delete(id);
        }
    }
}
