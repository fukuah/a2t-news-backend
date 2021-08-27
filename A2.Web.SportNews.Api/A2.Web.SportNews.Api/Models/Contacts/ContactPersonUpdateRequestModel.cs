using A2.Web.SportNews.Api.Models.Common;

namespace A2.Web.SportNews.Api.Models.Contacts
{
    public class ContactPersonUpdateRequestModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string FormalLink { get; set; }
        public FileInfoModel Photo { get; set; }
    }
}
