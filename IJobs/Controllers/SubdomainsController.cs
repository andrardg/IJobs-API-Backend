using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Services.SubdomainService;
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
    public class SubdomainsController : ControllerBase
    {
        private readonly ISubdomainService _service;
        public SubdomainsController(ISubdomainService service)
        {
            _service = service;
        }

        // GET: api/<SubdomainsController>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<SubdomainDTO> Get()
        {
            return _service.GetAllSubdomains();
        }

        [HttpGet]
        [Route("GetAll/{id}")]
        [AllowAnonymous]
        public IEnumerable<SubdomainDTO> GetAll(Guid id)
        {
            return _service.GetAllSubdomainsByDomainId(id);
        }

        // GET api/<SubdomainsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public SubdomainDTO Get(Guid id)
        {
            return _service.GetById(id);
        }

        // POST api/<SubdomainsController>
        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        public void Post([FromBody] SubdomainDTO subdomain)
        {
            _service.Create(subdomain);
        }

        // PUT api/<SubdomainsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public void Put(Guid id, [FromBody] SubdomainDTO subdomain)
        {
            subdomain.Id = id;
            _service.Update(subdomain);
            _service.Save();
        }

        // DELETE api/<SubdomainsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public void Delete(Guid? id)
        {
            _service.Delete(id);
            _service.Save();
        }
    }
}
