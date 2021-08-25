using System.Threading.Tasks;
using A2.Web.SportNews.Core;
using A2.Web.SportNews.Core.Requests;
using Microsoft.AspNetCore.Http;

namespace A2.Web.SportNews.Abstract
{
    public interface INewsService
    {
        Task<Pagination<NewsCore>> GetNewsPageAsync(NewsPageRequest request);

        Task<NewsCore> GetNewsByIdAsync(int id);

        Task AddArticle(NewsCore article, FileInfoCore fileInfo);
        void DeleteArticle(int id);
    }
}
