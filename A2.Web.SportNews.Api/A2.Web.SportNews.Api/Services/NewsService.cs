using System;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Api.Abstract;
using A2.Web.SportNews.Core;
using A2.Web.SportNews.Core.Extensions;
using A2.Web.SportNews.Core.Mappers;
using A2.Web.SportNews.Core.Requests;
using A2.Web.SportNews.Database;

namespace A2.Web.SportNews.Api.Services
{
    public class NewsService : INewsService
    {
        private readonly FileManageService _fileManageService;
        private readonly UnitOfWorkFactory _uowFactory;

        public NewsService(FileManageService fileManageService, UnitOfWorkFactory uowFactory)
        {
            _fileManageService = fileManageService ?? throw new ArgumentNullException(nameof(fileManageService));
            _uowFactory = uowFactory ?? throw new ArgumentNullException(nameof(uowFactory));
        }

        public async Task<Pagination<NewsCore>> GetPageAsync(NewsPageRequest request)
        {
            await using var uow = _uowFactory.Create();
            var news = await uow.NewsRepository.GetEntitiesAsync(request.Limit, request.Offset);

            return new Pagination<NewsCore>
            {
                Count = await uow.NewsRepository.CountAsync(),
                Offset = request.Offset,
                Limit = request.Limit,
                Items = news.Select(x => x.ToCore()).ToList()
            };
        }

        public async Task<NewsCore> GetByIdAsync(int id)
        {
            await using var uow = _uowFactory.Create();

            return (await uow.NewsRepository.GetEntityAsync(id)).ToCore();
        }

        public async Task AddAsync(NewsCore article, FileInfoCore fileInfo)
        {
            if (article == null)
                throw new ArgumentNullException(nameof(article));

            var hasNewFile = fileInfo.HasFile();
            if (hasNewFile)
                article.ImageLink = fileInfo.Name;

            await using var uow = _uowFactory.Create();
            await uow.NewsRepository.AddAsync(article.ToEntity());
            await uow.SaveChangesAsync();

            if(hasNewFile)
                await _fileManageService.UploadAsync(article.ImageLink, fileInfo.FileB64);
        }

        public async Task DeleteAsync(int id)
        {
            await using var uow = _uowFactory.Create();
            await uow.NewsRepository.DeleteAsync(id);
            await uow.SaveChangesAsync();
        }
    }
}
