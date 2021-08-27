using System;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Api.Mappers;
using A2.Web.SportNews.Api.Models.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A2.Web.SportNews.Api.Controllers
{
    [ApiController]
    [Route("contacts")]
    public class ContactPersonsController
    {
        private readonly IContactPersonsService _personsService;

        public ContactPersonsController(IContactPersonsService personsService)
        {
            _personsService = personsService ?? throw new ArgumentNullException(nameof(personsService));
        }

        [HttpGet]
        public async Task<ContactPersonsModel> GetAllAsync()
        {
            var contacts = await _personsService.GetAllAsync();

            return new ContactPersonsModel
            {
                ContactPersons = contacts.Select(x => x.ToModel()).ToList()
            };
        }

        [Authorize]
        [HttpDelete, Route("{id}")]
        public async Task<ActionResult> DeleteContact(int id)
        {
            await _personsService.DeleteContactAsync(id);

            return new OkResult();
        }

        [Authorize]
        [HttpPost, Route("create"), DisableRequestSizeLimit]
        public async Task<ActionResult> AddContactAsync(ContactPersonCreateRequestModel model)  
        {
            await _personsService.AddContactAsync(model.ToCore(), model.Photo.ToCore());

            return new OkResult();
        }

        [Authorize]
        [HttpPut, Route("{id}"), DisableRequestSizeLimit]
        public async Task<ActionResult> UpdateContactAsync(ContactPersonUpdateRequestModel model)
        {
            await _personsService.UpdateContactAsync(model.ToCore(), model.Photo.ToCore());

            return new OkResult();
        }
    }
}
