﻿namespace A2.Web.SportNews.Models.Contacts
{
    public class ContactPersonCreateRequestModel
    {
        public string FullName { get; set; }
        public string FormalLink { get; set; }
        public byte[] PhotoData { get; set; }
    }
}