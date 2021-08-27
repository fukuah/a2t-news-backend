using A2.Web.SportNews.Database.Entities;

namespace A2.Web.SportNews.Core.Mappers
{
    public static class ContactsMapper
    {
        public static ContactPersonCore ToCore(this ContactPersonEntity entity)
        {
            if (entity == null) return null;

            return new ContactPersonCore
            {
                Id = entity.Id,
                FullName = entity.FullName,
                PhotoLink = entity.ImageLink,
                FormalLink = entity.FormalLink
            };
        }

        public static ContactPersonEntity ToEntity(this ContactPersonCore core)
        {
            if (core == null) return null;

            return new ContactPersonEntity
            {
                Id = core.Id,
                FullName = core.FullName,
                ImageLink = core.PhotoLink,
                FormalLink = core.FormalLink
            };
        }
    }
}
