using System;
using System.Collections.Generic;
using System.Text;
using A2.Web.SportNews.Api.Models.Contacts;
using A2.Web.SportNews.Core;

namespace A2.Web.SportNews.Api.Mappers
{
    static class ContactsModelMapper
    {
        public static ContactPersonCore ToCore(this ContactPersonUpdateRequestModel model)
        {
            if (model == null) return null;

            return new ContactPersonCore
            {
                Id = model.Id,
                FullName = model.FullName,
                FormalLink = model.FormalLink,
            };
        }

        public static ContactPersonModel ToModel(this ContactPersonCore core)
        {
            if (core == null) return null;

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
            if (model == null) return null;

            return new ContactPersonCore
            {
                Id = model.Id,
                FullName = model.FullName,
                FormalLink = model.FormalLink
            };
        }
        public static ContactPersonCore ToCore(this ContactPersonCreateRequestModel model)
        {
            if (model == null) return null;

            return new ContactPersonCore
            {
                FullName = model.FullName,
                FormalLink = model.FormalLink
            };
        }
    }
}
