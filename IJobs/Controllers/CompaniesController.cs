using AutoMapper;
using IJobs.Models;
using IJobs.Models.DTOs;
using IJobs.Services;
using IJobs.Utilities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        public CompaniesController(ICompanyService service, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _service = service;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        [AllowAnonymous]
        public IEnumerable<CompanyResponseDTO> Get()
        {
            return _service.GetAllCompanies();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        [AllowAnonymous]
        public CompanyResponseDTO Get(Guid? id)
        {
            return _service.GetById(id);
        }
        [HttpGet]
        [Route("Search/{Name}")]
        [AllowAnonymous]
        public IEnumerable<CompanyResponseDTO> Search(string Name)
        {
            return _service.GetByName(Name);
        }
        // PUT api/<ValuesController>/5
        [Consumes("multipart/form-data")]
        [HttpPut("{id}")]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company))]
        public void Put(Guid? id, [FromForm, System.Web.Http.FromBody] CompanyRequestDTO company)
        {
            _service.Update(id, company);
            _service.Save();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        [Authorize(Roles = nameof(Role.Admin) + ", " + nameof(Role.Company))]
        public void Delete(Guid? id)
        {
            _service.Delete(id);
            _service.Save();
        }
    }
}
