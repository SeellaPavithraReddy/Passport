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
    public class VisaController : ControllerBase
    {
        private readonly PassportDBContext _context;
        VisaBAO visaBAO = null;
        public VisaController(PassportDBContext context)
        {
            _context = context;
            visaBAO = new VisaBAO(_context);
        }

        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            List<Visa> visas = visaBAO.getAll();

            if (visas.Count != 0)
            {
                return Ok(visas);
            }
            else
            {
                return NotFound("no records");
            }
        }


        [HttpGet]
        [Route("GetAllVisaApp")]
        public IActionResult GetAllVisaApp()
        {
            List<VisaApplicationCost> vlist = visaBAO.getallVisaApp();
            if (vlist.Count != 0)
            {
                return Ok(vlist);
            }
            else
            {
                return NotFound("no records ");
            }
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] Visa visa)
        {
            string visaInsert = visaBAO.insert(visa);
            if (visaInsert != null)
            {
                return Ok(visaInsert);
            }
            else
            {
                return BadRequest(visaInsert);
            }

        }

        [HttpPut]
        [Route("Cancel/{visaId}")]
        public IActionResult Cancel([FromBody] Visa visa, string visaId)
        {
            var result = visaBAO.cancel(visa, visaId);
            if (result == null)
            {
                return BadRequest("Invaild");
            }
            else
            {
                return Ok(result);
            }
        }

        
        [HttpGet]
        [Route("GetByVisaId/{visaId}")]
        public IActionResult GetByVisaId(string visaId)
        {
            Visa? visa = visaBAO.getByVisaId(visaId);
            if (visa != null)
            {
                return Ok(visa);
            }
            else
            {
                return NotFound("No data " + visaId + " found");
            }
        }
    }
}