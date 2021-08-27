using A2.Web.SportNews.Api.Models.Login;
using A2.Web.SportNews.Api.Models.News;
using A2.Web.SportNews.Auth.Core.Requests;
using A2.Web.SportNews.Core.Requests;

namespace A2.Web.SportNews.Api.Mappers
{
    public static class RequestMapper
    {
        public static NewsPageRequest ToCore(this NewsPageRequestModel model)
        {
            if (model == null) return null;

            return new NewsPageRequest
            {
                Limit = model.Limit,
                Offset = model.Offset
            };
        }

        public static LoginRequest ToRequest(this LoginRequestModel model)
        {
            return new LoginRequest
            {
                Password = model.Password,
                Username = model.Username
            };
        }
    }
}
