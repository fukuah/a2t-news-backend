using System.Collections.Generic;

namespace A2.Web.SportNews.Models.Common
{
    public class PaginationModel<TModel> 
    {
        public int Limit { get; set; }
        public int Offset { get; set; }
        public int Count { get; set; }

        public ICollection<TModel> Models { get; set; }
    }
}
