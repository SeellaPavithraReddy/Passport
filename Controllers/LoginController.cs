using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PVMS_Project.Data;
using PVMS_Project.Models.BAO;

namespace PVMS_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly PassportDBContext _context;
        RegisterBAO registerBAO = null;
        LoginBAO loginBAO = null;
        public LoginController(PassportDBContext context)
        {
            this._context = context;
            registerBAO = new RegisterBAO(_context);
            loginBAO = new LoginBAO(_context);
        }


        [HttpGet]
        [Route("ValidateLogin/{userId}/{password}")]
        public IActionResult Login(string userId, string password)
        {

            try
            {
                var loginDetails = loginBAO.validateUser(userId, password);
                Console.WriteLine($"Login Details: {loginDetails}"); // Login output

                if (loginDetails.Split(",")[0].Equals("valid"))
                {
                    return Ok(loginDetails.Split(",")[1]);
                }
                else
                {
                    return BadRequest(loginDetails);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex.Message}"); // Login exception
                return StatusCode(500, "Internal server error");
            }
        }

    }
}