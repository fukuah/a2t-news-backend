using System.Threading.Tasks;
using A2.Web.SportNews.Core;
using A2.Web.SportNews.Core.Requests;

namespace A2.Web.SportNews.Api.Abstract
{
    public interface INewsService
    {
        Task<Pagination<NewsCore>> GetPageAsync(NewsPageRequest request);
        Task<NewsCore> GetByIdAsync(int id);
        Task AddAsync(NewsCore article, FileInfoCore fileInfo);
        Task DeleteAsync(int id);
    }
}
