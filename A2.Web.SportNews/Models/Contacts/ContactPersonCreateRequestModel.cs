﻿using A2.Web.SportNews.Models.Common;

namespace A2.Web.SportNews.Models.Contacts
{
    public class ContactPersonCreateRequestModel
    {
        public string FullName { get; set; }
        public string FormalLink { get; set; }
        //public string PhotoLink { get; set; }
        public FileInfoModel Photo { get; set; }
    }
}
