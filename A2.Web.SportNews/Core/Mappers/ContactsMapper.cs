using A2.Web.SportNews.Entities;
using A2.Web.SportNews.Models.Contacts;

namespace A2.Web.SportNews.Core.Mappers
{
    public static class ContactsMapper
    {
        public static ContactPersonModel ToModel(this ContactPersonCore core)
        {
            return new ContactPersonModel
            {
                Id = core.Id,
                FullName = core.FullName,
                PhotoFile = core.PhotoLink,
                FormalLink = core.FormalLink
            };
        }

        public static ContactPersonCore ToCore(this ContactPersonModel model)
        {
            return new ContactPersonCore
            {
                Id = model.Id,
                FullName = model.FullName,
                FormalLink = model.FormalLink
            };
        }
        public static ContactPersonCore ToCore(this ContactPersonCreateRequestModel model)
        {
            return new ContactPersonCore
            {
                FullName = model.FullName,
                FormalLink = model.FormalLink
            };
        }

        public static ContactPersonCore ToCore(this ContactPersonEntity model)
        {
            return new ContactPersonCore
            {
                Id = model.Id,
                FullName = model.FullName,
                PhotoLink = model.ImageLink,
                FormalLink = model.FormalLink
            };
        }

        public static ContactPersonEntity ToEntity(this ContactPersonCore model)
        {
            return new ContactPersonEntity
            {
                Id = model.Id,
                FullName = model.FullName,
                ImageLink = model.PhotoLink,
                FormalLink = model.FormalLink
            };
        }
    }
}
