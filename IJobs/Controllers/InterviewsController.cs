using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Services.InterviewService;
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
    public class InterviewsController : ControllerBase
    {
        private readonly IInterviewService _service;
        public InterviewsController(IInterviewService service)
        {
            _service = service;
        }
        // GET: api/<InterviewController>
        [HttpGet]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company) + ", " + nameof(Role.User))]
        public IEnumerable<InterviewDTO> Get()
        {
            return _service.GetAllInterviewsJoin();
        }

        // GET api/<InterviewController>/5
        [HttpGet("{id}")]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company) + ", " + nameof(Role.User))]
        public InterviewDTO Get(Guid id)
        {
            return _service.GetByIdWithJoin(id);
        }

        // POST api/<InterviewController>
        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company))]
        public void Post([FromBody] InterviewDTO interview)
        {
            _service.Create(interview);
        }

        // PUT api/<InterviewController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company) + ", " + nameof(Role.User))]
        public void Put(Guid id, [FromBody] InterviewDTO interview)
        {
            interview.Id = id;
            _service.Update(interview);
            _service.Save();
        }

        // DELETE api/<InterviewController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _service.Delete(id);
            _service.Save();
        }
    }
}
