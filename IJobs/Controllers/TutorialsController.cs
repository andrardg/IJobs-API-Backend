using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Services.TutorialService;
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
    public class TutorialsController : ControllerBase
    {
        private readonly ITutorialService _service;
        public TutorialsController(ITutorialService service)
        {
            _service = service;
        }
        // GET: api/<TutorialsController>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<TutorialDTO> Get()
        {
            return _service.GetAllTutorials();
        }
        [HttpGet]
        [AllowAnonymous]
        [Route("GetAll/{id}")]
        public IEnumerable<TutorialDTO> GetAll(Guid id)
        {
            return _service.GetAllTutorialsBySubdomainId(id);
        }

        // GET api/<TutorialsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public TutorialDTO Get(Guid id)
        {
            return _service.GetById(id);
        }

        // POST api/<TutorialsController>
        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin))]
        public void Post([FromBody] TutorialDTO tutorial)
        {
            _service.Create(tutorial);
        }

        // PUT api/<TutorialsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public void Put(Guid id, [FromBody] TutorialDTO tutorial)
        {
            tutorial.Id = id;
            _service.Update(tutorial);
            _service.Save();
        }

        // DELETE api/<TutorialsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin))]
        public void Delete(Guid id)
        {
            _service.Delete(id);
            _service.Save();
        }
    }
}
