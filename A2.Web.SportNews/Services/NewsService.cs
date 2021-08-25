using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Core;
using A2.Web.SportNews.Core.Mappers;
using A2.Web.SportNews.Core.Requests;
using A2.Web.SportNews.Entities;
using Microsoft.AspNetCore.Http;

namespace A2.Web.SportNews.Services
{
    public class NewsService : INewsService
    {
        private readonly IRepository<NewsEntity> _newsRepository;
        private readonly FileUploadService _fileUploadService;

        public NewsService(IRepository<NewsEntity> newsRepository, FileUploadService fileUploadService)
        {
            _newsRepository = newsRepository ?? throw new ArgumentNullException(nameof(newsRepository));
            _fileUploadService = fileUploadService ?? throw new ArgumentNullException(nameof(fileUploadService));
        }

        public async Task<Pagination<NewsCore>> GetNewsPageAsync(NewsPageRequest request)
        {
            var news = await _newsRepository.GetEntities(request.Limit, request.Offset);

            return new Pagination<NewsCore>
            {
                Count = _newsRepository.Count(),
                Offset = request.Offset,
                Limit = request.Limit,
                Items = news.Select(x => x.ToCore()).ToList()
            };
        }

        public async Task<NewsCore> GetNewsByIdAsync(int id) => (await _newsRepository.GetEntity(id)).ToCore();

        public async Task AddArticle(NewsCore article, FileInfoCore fileInfo)
        {
            var hasNewFile = fileInfo != null && !string.IsNullOrWhiteSpace(fileInfo.Name) && !string.IsNullOrWhiteSpace(fileInfo.FileB64);
            if (hasNewFile)
                article.ImageLink = fileInfo.Name;

            _newsRepository.Add(article.ToEntity());

            if(hasNewFile)
                await _fileUploadService.Upload(article.ImageLink, fileInfo.FileB64);
        }

        public void DeleteArticle(int id)
        {
            _newsRepository.Delete(id);
        }
    }
}
