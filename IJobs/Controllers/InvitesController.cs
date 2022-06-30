using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Services.InviteService;
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
    [ApiController]
    [Authorize]
    public class InvitesController : ControllerBase
    {
        private readonly IInviteService _service;
        public InvitesController(IInviteService service)
        {
            _service = service;
        }

        // GET: api/<InvitesController>
        [HttpGet]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company) + ", " + nameof(Role.User))]
        public IEnumerable<InviteDTO> Get()
        {
            return _service.GetAllInvitesJoin();
        }

        // GET api/<InvitesController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company) + ", " + nameof(Role.User))]
        public InviteDTO Get(Guid id)
        {
            return _service.GetByIdWithJoin(id);
        }

        // POST api/<InvitesController>
        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company) + ", " + nameof(Role.User))]
        public void Post([FromBody] InviteDTO invite)
        {
            _service.Create(invite);
        }

        // DELETE api/<InvitesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company) + ", " + nameof(Role.User))]
        public void Delete(Guid id)
        {
            _service.Delete(id);
            _service.Save();
        }
    }
}
