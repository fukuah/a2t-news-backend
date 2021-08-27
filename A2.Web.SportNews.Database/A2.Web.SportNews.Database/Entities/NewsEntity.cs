using System;

namespace A2.Web.SportNews.Database.Entities
{
    public class NewsEntity : EntityWithAttachment
    {
        public string Title { get; set; }
        public string TextPreview { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
