using System;
using System.Collections.Generic;
using System.IO;
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
        private readonly FileUploadService _fileUploadService;

        public ContactPersonsService(IRepository<ContactPersonEntity> repository, FileUploadService fileUploadService)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _fileUploadService = fileUploadService ?? throw new ArgumentNullException(nameof(fileUploadService));
        }

        public async Task<ICollection<ContactPersonCore>> GetAll()
        {
            return (await _repository.GetEntities())?.Select(x => x.ToCore()).ToList();
        }

        public async Task UpdateContact(ContactPersonCore contact, string fileB64)
        {
            if (contact.Id == default)
                throw new ArgumentException(nameof(contact));

            if (!string.IsNullOrWhiteSpace(fileB64))
                contact.PhotoLink = Path.GetRandomFileName() + ".png";

            _repository.Update(contact.ToEntity());

            if (!string.IsNullOrWhiteSpace(contact.PhotoLink))
                await _fileUploadService.Upload(contact.PhotoLink, fileB64);
        }

        public async Task AddContact(ContactPersonCore contact, string fileB64)
        {
            if (!string.IsNullOrWhiteSpace(fileB64))
                contact.PhotoLink = Path.GetRandomFileName() + ".png";

            _repository.Add(contact.ToEntity());

            if (!string.IsNullOrWhiteSpace(contact.PhotoLink))
                await _fileUploadService.Upload(contact.PhotoLink, fileB64);
        }

        public void DeleteContact(int id)
        {
            _repository.Delete(id);
        }
    }
}
