using System;

namespace A2.Web.SportNews.Api.Models.News
{
    public class NewsPreviewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TextPreview { get; set; }
        public string ImageLink { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
