using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Core;
using A2.Web.SportNews.Core.Mappers;
using A2.Web.SportNews.Entities;

namespace A2.Web.SportNews.Services
{
    public class ContactPersonsService : IContactPersonsService
    {
        private readonly IRepository<ContactPersonEntity> _repository;

        public ContactPersonsService(IRepository<ContactPersonEntity> repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<ICollection<ContactPersonCore>> GetAll()
        {
            return (await _repository.GetEntities())?.Select(x => x.ToCore()).ToList();
        }

        public void UpdateContact(ContactPersonCore contact)
        {
            _repository.Update(contact.ToEntity());
        }

        public void AddContact(ContactPersonCore contact)
        {
            _repository.Add(contact.ToEntity());
        }

        public void DeleteContact(int id)
        {
            _repository.Delete(id);
        }
    }
}
