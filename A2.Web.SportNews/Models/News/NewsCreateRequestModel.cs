using System;

namespace A2.Web.SportNews.Models.News
{
    public class NewsCreateRequestModel
    {
        public string Title { get; set; }
        public string TextPreview { get; set; }
        public string Content { get; set; }
        public byte[] ImageData { get; set; }
        public DateTime? PublishDate { get; set; }
    }
}
