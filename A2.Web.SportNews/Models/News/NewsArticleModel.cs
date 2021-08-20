using System;

namespace A2.Web.SportNews.Models.News
{
    /// <summary>
    /// Для отображения новости на отдельной странице
    /// </summary>
    
    public class NewsArticleModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImageLink { get; set; }
        public DateTime PublishDate { get; set; }
    }
}
