using IJobs.Models.DTOs;
using IJobs.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IJobs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ICompanyService _companyService;
        public AccountController(IUserService userService, ICompanyService companyService)
        {
            _userService = userService;
            _companyService = companyService;
        }
        [HttpPost]
        [Route("Login")]
        public AccountDTO Login([System.Web.Http.FromBody] AccountDTO account)
        {
            AccountDTO response1, response2 = new AccountDTO();
            response1 = _userService.Authenticate(account);
            if (response1.Email == null)
            {
                response2 = _companyService.Authenticate(account);
                if (response2.Email == null)
                    throw new Exception("Email or password is incorrect");
                else
                {
                    response2.Type = "company";
                    return response2;
                }
            }
            else
            {
                response1.Type = "user";
                return response1;
            }
        }
        [HttpPost]
        [Route("Register")]
        public void Register([System.Web.Http.FromBody] AccountDTO account)
        {
            if (account.Type == "user")
                _userService.Register(account);
            else if (account.Type == "company")
                _companyService.Register(account);
        }
    }
}
