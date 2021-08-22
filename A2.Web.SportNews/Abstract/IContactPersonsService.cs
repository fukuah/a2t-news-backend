using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Core;

namespace A2.Web.SportNews.Abstract
{
    public interface IContactPersonsService
    {
        Task<ICollection<ContactPersonCore>> GetAll();
        Task UpdateContact(ContactPersonCore contact, string photoFile);
        Task AddContact(ContactPersonCore contact, string photoFile);
        void DeleteContact(int id);
    }
}
