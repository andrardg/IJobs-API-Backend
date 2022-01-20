using AutoMapper;
using IJobs.Models.DTOs;
using IJobs.Services;
using IJobs.Utilities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IJobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _service;
        private readonly IMapper _mapper;

        public JobsController(IJobService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        // GET: api/<JobsController>
        [HttpGet]
        public IEnumerable<JobDTO> Get()
        {
            return _service.GetAllJobs();
        }

        // GET api/<JobsController>/5
        [HttpGet("{id}")]
        public JobDTO Get(Guid id)
        {
            return _service.FindById(id);
        }

        // POST api/<JobsController>
        [HttpPost]
        public void Post([System.Web.Http.FromBody] JobDTO job)
        {
            _service.Create(job);
        }

        // PUT api/<JobsController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [System.Web.Http.FromBody] JobDTO job)
        {
            job.Id = id;
            _service.Update(job);
            _service.Save();
        }

        // DELETE api/<JobsController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id, [System.Web.Http.FromBody] JobDTO job)
        {
            _service.Delete(id);
            _service.Save();
        }
    }
}
