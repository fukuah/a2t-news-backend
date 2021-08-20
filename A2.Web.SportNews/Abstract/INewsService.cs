using System.Threading.Tasks;
using A2.Web.SportNews.Core;
using A2.Web.SportNews.Core.Requests;

namespace A2.Web.SportNews.Abstract
{
    public interface INewsService
    {
        Task<Pagination<NewsCore>> GetNewsPageAsync(NewsPageRequest request);

        Task<NewsCore> GetNewsByIdAsync(int id);

        void AddArticle(NewsCore article);
        void DeleteArticle(int id);
    }
}
