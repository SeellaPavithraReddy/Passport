using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PVMS_Project.Data;
using PVMS_Project.Models.BAO;
using PVMS_Project.Models.ENTITIES;

namespace PVMS_Project.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RegisterController : ControllerBase
    {
        private readonly PassportDBContext _context;
        private RegisterBAO registerBAO = null;

        public RegisterController(PassportDBContext context)
        {
            _context = context;
            registerBAO = new RegisterBAO(_context);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            List<Register> registerList = registerBAO.getAll();
            if (registerList.Count != 0)
            {
                return Ok(registerList);
            }
            else
            {
                return NotFound("no records ");
            }
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] Register register)
        {
            string registerData = registerBAO.insert(register);

            if (registerData != null)
            {
                string[] parts = registerData.Split(',');

                if (parts.Length >= 3)
                {
                    return Ok(registerData);
                }
                else
                {
                    return BadRequest(registerData);
                }
            }
            else
            {
                return BadRequest("Invalid data");
            }
        }


        [HttpPut]
        [Route("ForgotPassword/{userId}/{password}")]
        public IActionResult ForgotPassword(string userId, string password)
        {
            string result = registerBAO.forgotPassword(userId, password);
            if (result == "Password updated successfully")
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}