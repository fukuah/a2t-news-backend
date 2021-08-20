namespace A2.Web.SportNews.Models.Contacts
{
    public class ContactPersonEditRequestModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FormalLink { get; set; }
        public byte[] PhotoData { get; set; }
    }
}
