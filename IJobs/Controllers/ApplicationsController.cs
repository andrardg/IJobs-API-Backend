using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Services.ApplicationService;
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
    public class ApplicationsController : ControllerBase
    {
        private readonly IApplicationService _service;
        public ApplicationsController(IApplicationService service)
        {
            _service = service;
        }

        // GET: api/<ApplicationController>
        [HttpGet]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company) + ", " + nameof(Role.User))]
        public IEnumerable<ApplicationDTO> Get()
        {
            return _service.GetAllApplicationsJoin();
        }

        // GET api/<ApplicationController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company) + ", " + nameof(Role.User))]
        public ApplicationDTO Get(Guid id)
        {
            return _service.GetByIdWithJoin(id);
        }

        // POST api/<ApplicationController>
        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.User))]
        public void Post([FromBody] ApplicationDTO app)
        {
            _service.Create(app);
        }

        // PUT api/<ApplicationController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company))]
        public void Put(Guid id, [FromBody] ApplicationDTO app)
        {
            app.Id = id;
            _service.Update(app);
            _service.Save();
        }

        // DELETE api/<ApplicationController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.User))]
        public void Delete(Guid id)
        {
            _service.Delete(id);
            _service.Save();
        }
    }
}
