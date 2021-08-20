using A2.Web.SportNews.Core.Requests;
using A2.Web.SportNews.Models.News;

namespace A2.Web.SportNews.Core.Mappers
{
    public static class RequestMapper
    {
        public static NewsPageRequest ToCore(this NewsPageRequestModel model)
        {
            return new NewsPageRequest
            {
                Limit = model.Limit,
                Offset = model.Offset
            };
        }

    }
}
