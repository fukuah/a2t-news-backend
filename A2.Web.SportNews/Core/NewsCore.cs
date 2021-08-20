﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2.Web.SportNews.Core
{
    public class NewsCore
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TextPreview { get; set; }
        public string Content { get; set; }
        public string ImageLink { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
