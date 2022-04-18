using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using IJobs.Services;
using IJobs.Utilities;
using Microsoft.Extensions.Options;
using IJobs.Models.DTOs;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using IJobs.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace IJobs.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;
        private readonly AppSettings _appSettings;
        public UsersController(IUserService service, IMapper mapper, IOptions<AppSettings> appSettings)
        {
            _service = service;
            _mapper = mapper;
            _appSettings = appSettings.Value;
        }
        // GET: api/<UsersController>
        [HttpGet]
        //[AllowAnonymous]
        [Authorize(Roles = nameof(Role.Admin))]
        //[Authorize(Policy = "admin")]
        public IEnumerable<UserResponseDTO> Get()
        {
            return _service.GetAllUsers();
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public UserResponseDTO Get(Guid? id)
        {
            return _service.GetById(id);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("Login")]
        public UserResponseDTO Login([System.Web.Http.FromBody] UserRequestDTO user)
        {
            return _service.Authenticate(user);
        }

        // POST api/<UsersController>
        [AllowAnonymous]
        [HttpPost]
        [Route("Register")]
        public void Register([System.Web.Http.FromBody] UserRequestDTO user)
        {
            _service.Register(user);
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public void Put(Guid id, [System.Web.Http.FromBody] UserRequestDTO user)
        {
            _service.Update(id, user);
            _service.Save();
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid? id)
        {
            _service.Delete(id);
            _service.Save();
        }
    }
}
