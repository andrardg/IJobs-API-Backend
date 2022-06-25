using AutoMapper;
using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Services;
using IJobs.Utilities;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize]
    public class JobsController : ControllerBase
    {
        private readonly IJobService _service;
        public JobsController(IJobService service)
        {
            _service = service;
        }

        // GET: api/<JobsController>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<JobDTO> Get()
        {
            return _service.GetAllJobsWithCompany();
        }
        [HttpGet]
        [Route("GetAllWork")]
        [AllowAnonymous]
        public IEnumerable<JobDTO> GetWork()
        {
            return _service.GetAllWorkWithCompany();
        }

        // GET api/<JobsController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public JobDTO Get(Guid id)
        {
            return _service.GetById(id);
        }
        [HttpGet]
        [Route("Search/{Name}")]
        [AllowAnonymous]
        public IEnumerable<JobDTO> Search(string Name)
        {
            return _service.GetByJobTitleWithCompany(Name);
        }
        // POST api/<JobsController>
        [HttpPost]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company))]
        public void Post([System.Web.Http.FromBody] JobDTO job)
        {
            _service.Create(job);
        }

        // PUT api/<JobsController>/5
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company))]
        public void Put(Guid id, [System.Web.Http.FromBody] JobDTO job)
        {
            job.Id = id;
            _service.Update(job);
            _service.Save();
        }

        // DELETE api/<JobsController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company))]
        public void Delete(Guid? id)
        {
            _service.Delete(id);
            _service.Save();
        }
    }
}
