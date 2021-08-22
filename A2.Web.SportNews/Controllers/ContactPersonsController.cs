using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Core.Mappers;
using A2.Web.SportNews.Models.Contacts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace A2.Web.SportNews.Controllers
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
        public async Task<ContactPersonsModel> GetAll()
        {
            var contacts = await _personsService.GetAll();

            return new ContactPersonsModel
            {
                ContactPersons = contacts.Select(x => x.ToModel()).ToList()
            };
        }

        [Authorize]
        [HttpDelete, Route("{id}")]
        public ActionResult DeleteContact(int id)
        {
            _personsService.DeleteContact(id);

            return new OkResult();
        }

        [Authorize]
        [HttpPost, Route("create"), DisableRequestSizeLimit]
        public ActionResult AddContact(ContactPersonModel model)
        {
            _personsService.AddContact(model.ToCore(), model.PhotoFile);

            return new OkResult();
        }

        [Authorize]
        [HttpPut, Route("{id}"), DisableRequestSizeLimit]
        public ActionResult UpdateContact(ContactPersonModel model)
        {
            _personsService.UpdateContact(model.ToCore(), model.PhotoFile);

            return new OkResult();
        }
    }
}
