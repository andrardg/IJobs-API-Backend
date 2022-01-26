using AutoMapper;
using IJobs.Models.DTOs;
using IJobs.Services;
using Microsoft.AspNetCore.Cors;
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
    public class CompaniesController : ControllerBase
    {
        private readonly ICompanyService _service;
        private readonly IMapper _mapper;
        public CompaniesController(ICompanyService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        //[EnableCors]//("AllowOrigin")]
        public IEnumerable<CompanyResponseDTO> Get()
        {
            return _service.GetAllCompanies();
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public CompanyResponseDTO Get(Guid? id)
        {
            return _service.FindById(id);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public void Post([System.Web.Http.FromBody] CompanyRequestDTO company)
        {
            _service.Create(company);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [System.Web.Http.FromBody] CompanyRequestDTO company)
        {
            company.Id = id;
            _service.Update(company);
            _service.Save();
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid? id)
        {
            _service.Delete(id);
            _service.Save();
        }
    }
}
