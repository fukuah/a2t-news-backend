using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A2.Web.SportNews.Abstract;
using A2.Web.SportNews.Core.Mappers;
using A2.Web.SportNews.Models.Contacts;
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

        [HttpDelete, Route("{id}")]
        public ActionResult DeleteContact(int id)
        {
            _personsService.DeleteContact(id);

            return new OkResult();
        }

        [HttpPost, Route("create")]
        public ActionResult AddContact(ContactPersonModel model)
        {
            _personsService.AddContact(model.ToCore());

            return new OkResult();
        }

        [HttpPut, Route("{id}")]
        public ActionResult UpdateContact(ContactPersonModel model)
        {
            _personsService.UpdateContact(model.ToCore());

            return new OkResult();
        }
    }
}
