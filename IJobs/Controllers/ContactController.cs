using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Services.ContactService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IJobs.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactService _service;
        public ContactController(IContactService service)
        {
            _service = service;
        }

        // GET: api/<ContactController>
        [HttpGet]
        [Authorize(Roles = nameof(Role.Admin))]
        public IEnumerable<ContactDTO> Get()
        {
            return (IEnumerable<ContactDTO>)_service.GetAll();
        }

        // POST api/<ContactController>
        [HttpPost]
        [AllowAnonymous]
        public void Post([FromBody] ContactDTO contact)
        {
            _service.Create(contact);
        }

        // PUT api/<ContactController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public void Put(Guid id, [FromBody] ContactDTO contact)
        {
            contact.Id = id;
            _service.Update(contact);
            _service.Save();
        }

        // DELETE api/<ContactController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public void Delete(Guid id)
        {
            _service.Delete(id);
            _service.Save();
        }
    }
}
