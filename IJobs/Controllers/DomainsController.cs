using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Services.DomainService;
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
    public class DomainsController : ControllerBase
    {
        private readonly IDomainService _service;
        public DomainsController(IDomainService service)
        {
            _service = service;
        }

        // GET: api/<DomainsController>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<DomainDTO> Get()
        {
            return _service.GetAllDomainsWithSubdomains();
        }

        // GET api/<DomainsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public DomainDTO Get(Guid id)
        {
            return _service.GetById(id);
        }

        // POST api/<DomainsController>
        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        public void Post([FromBody] DomainDTO domain)
        {
            _service.Create(domain);
        }

        // PUT api/<DomainsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public void Put(Guid id, [FromBody] DomainDTO domain)
        {
            domain.Id = id;
            _service.Update(domain);
            _service.Save();
        }

        // DELETE api/<DomainsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public void Delete(Guid id)
        {
            _service.Delete(id);
            _service.Save();
        }
    }
}
