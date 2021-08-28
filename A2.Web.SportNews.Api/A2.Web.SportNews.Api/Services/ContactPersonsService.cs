using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Core;
using A2.Web.SportNews.Core.Extensions;
using A2.Web.SportNews.Core.Mappers;
using A2.Web.SportNews.Database;

namespace A2.Web.SportNews.Api.Services
{
    public class ContactPersonsService : IContactPersonsService
    {
        private readonly UnitOfWorkFactory _uowFactory;
        private readonly FileManageService _fileManageService;

        public ContactPersonsService(FileManageService fileManageService, UnitOfWorkFactory uowFactory)
        {
            _fileManageService = fileManageService ?? throw new ArgumentNullException(nameof(fileManageService));
            _uowFactory = uowFactory ?? throw new ArgumentNullException(nameof(uowFactory));
        }

        public async Task<ICollection<ContactPersonCore>> GetAllAsync()
        {
            await using var uow = _uowFactory.Create();
            return (await uow.ContactsRepository.GetEntitiesAsync())?.Select(x => x.ToCore()).ToList();
        }


        public async Task UpdateContactAsync(ContactPersonCore contact, FileInfoCore fileInfo)
        {
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));
            if (contact.Id == default)
                throw new ArgumentException(nameof(contact));

            var hasNewFile = fileInfo.HasFile();
            if (hasNewFile)
                contact.PhotoLink = fileInfo.Name;

            await using var uow = _uowFactory.Create();
            await uow.ContactsRepository.UpdateAsync(contact.ToEntity());
            await uow.SaveChangesAsync();

            if (hasNewFile && !string.IsNullOrWhiteSpace(contact.PhotoLink))
                await _fileManageService.UploadAsync(contact.PhotoLink, fileInfo.FileB64);
        }

        public async Task AddContactAsync(ContactPersonCore contact, FileInfoCore fileInfo)
        {
            if (contact == null)
                throw new ArgumentNullException(nameof(contact));

            var hasNewFile = fileInfo.HasFile();
            if (hasNewFile)
                contact.PhotoLink = fileInfo.Name;

            await using var uow = _uowFactory.Create();
            await uow.ContactsRepository.AddAsync(contact.ToEntity());
            await uow.SaveChangesAsync();

            if (hasNewFile && !string.IsNullOrWhiteSpace(contact.PhotoLink))
                await _fileManageService.UploadAsync(contact.PhotoLink, fileInfo.FileB64);
        }

        public async Task DeleteContactAsync(int id)
        {
            await using var uow = _uowFactory.Create();
            await uow.ContactsRepository.DeleteAsync(id);
        }
    }
}
