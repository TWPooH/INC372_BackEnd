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
    public class ForgotPasswordController : ControllerBase
    {

        private readonly ILogger<ForgotPasswordController > _logger;
        private readonly DatabaseContext _databaseContext;

        public ForgotPasswordController(ILogger<ForgotPasswordController> logger, DatabaseContext databaseContext)
        {
            _logger = logger;
            _databaseContext = databaseContext;
        }

        [HttpPut]      // -> http://localhost:5293/User
        public IActionResult UpdatePassword(ForgotPassword forgotPassword)
        {
            try
            {
                var _forgotPassword = _databaseContext.Users.SingleOrDefault(e => e.Id == forgotPassword.Id);
                if(_forgotPassword != null)
                {
                    _forgotPassword.Password = forgotPassword.Password;

                    _databaseContext.Users.Update(_forgotPassword);  
                    _databaseContext.SaveChanges(); 
                    return Ok(new {message = "Change Password for this Username successful"});
                }
                else
                {
                    return Ok(new {message = "Change Password fail"});
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new{result = ex.Message, message = "fail"});
            }
        }  
    }
}