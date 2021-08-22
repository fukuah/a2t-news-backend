using System;
using Microsoft.AspNetCore.Http;

namespace A2.Web.SportNews.Models.News
{
    public class NewsCreateRequestModel
    {
        public string Title { get; set; }
        public string TextPreview { get; set; } 
        public string Content { get; set; }
        public string ImageFile { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}
