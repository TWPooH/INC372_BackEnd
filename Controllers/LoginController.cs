using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebAPI.DBContext;
using WebAPI.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {


        private readonly ILogger<LoginController > _logger;
        private readonly DatabaseContext _databaseContext;

        public LoginController(ILogger<LoginController> logger, DatabaseContext databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        [HttpPost]
        public IActionResult Login(Login login)
        {
            try
            {
                var _login = _databaseContext.Users.SingleOrDefault(a => a.Username == login.Username 
                && a.Password == login.Password);

                if( _login != null )
                {
                    return Ok(new {message= "Login Successful"});
                }
                else
                {
                return Ok(new {message= "Username Not Found or Password Wrong"});
                }

            }
            catch (Exception ex)
            {
                return StatusCode(401,new {result = ex.Message, message= "Logout Unsuccessful"});
            }
        }
    }
}