using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Core;

namespace A2.Web.SportNews.Abstract
{
    public interface IContactPersonsService
    {
        Task<ICollection<ContactPersonCore>> GetAllAsync();
        Task UpdateContactAsync(ContactPersonCore contact, FileInfoCore fileInfo);
        Task AddContactAsync(ContactPersonCore contact, FileInfoCore fileInfo);
        Task DeleteContactAsync(int id);
    }
}
