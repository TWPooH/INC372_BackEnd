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
    public class LogoutController : ControllerBase
    {


        private readonly ILogger<LogoutController > _logger;
        private readonly DatabaseContext _databaseContext;

        public LogoutController(ILogger<LogoutController> logger, DatabaseContext databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        [HttpPost]
        public IActionResult Logout(Logout login)
        {
            try
            {
                var _logout = _databaseContext.Users.SingleOrDefault(b => b.Username == login.Username);

                if( _logout != null )
                {
                    return Ok(new {message= "Logout Successful"});
                }
                else
                {
                return Ok(new {message= "Wrong Username, Please Try to logout again"});
                }

            }
            catch (Exception ex)
            {
                return StatusCode(401,new {result = ex.Message, message= "Logout Unsuccessful"});
            }
        }
    }
}