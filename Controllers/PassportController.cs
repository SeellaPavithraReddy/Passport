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
    public class PassportController : ControllerBase
    {
        private readonly PassportDBContext _context;
        PassportBAO passportBAO = null;
        public PassportController(PassportDBContext context)
        {
            _context = context;
            passportBAO = new PassportBAO(_context);
        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            List<Passport> passportlist = passportBAO.getAllPassport();
            if (passportlist.Count != 0)
            {
                return Ok(passportlist);
            }
            else
            {
                return NotFound("no records ");
            }
        }

        [HttpGet]
        [Route("GetAllStates")]
        public IActionResult GetAllStates()
        {
            List<State> statelist = passportBAO.getAllState();
            if (statelist.Count != 0)
            {
                return Ok(statelist);
            }
            else
            {
                return NotFound("no records ");
            }
        }



        [HttpGet]
        [Route("GetByPassportId/{PassportId}")]
        public IActionResult GetByPassportId(string PassportId)
        {
            Passport? passport = passportBAO.getByPassportId(PassportId);
            if (passport != null)
            {
                return Ok(passport);
            }
            else
            {
                return NotFound("No data" + PassportId + "found");
            }
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] Passport passport)
        {
            string s = passportBAO.insert(passport);
            if (s.Equals("1"))
            {
                return Ok("Need the passport number while giving payment? Please note down your Passport Id  " + passport.Passport_id + " Passport application cost is " + passport.ApplicationCost + " Rs. <Amount>");
            }
            else
            {
                return BadRequest(s);
            }
        }
        [HttpPut]
        [Route("Update/{passportid}")]
        public IActionResult Update([FromBody] Passport passport, string passportid)
        {
            string data = passportBAO.update(passport, passportid);
            if (data != null)
            {
                return Ok(data);
            }
            else
            {
                return BadRequest(passport);
            }
        }
    }
}