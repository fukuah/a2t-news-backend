﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace A2.Web.SportNews.Entities
{
    public class NewsEntity : EntityWithImage
    {
        public string Title { get; set; }
        public string TextPreview { get; set; }
        public string Content { get; set; }
        public DateTime PublishDate { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
