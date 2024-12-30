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
    public class VisaApplicationCostController : ControllerBase
    {
        private readonly PassportDBContext _context;
        VisaApplicationCostBAO vcostbao = null;
        public VisaApplicationCostController(PassportDBContext context)
        {
            _context = context;
            vcostbao = new VisaApplicationCostBAO(_context);
        }

        [HttpPost]
        [Route("Insert")]
        public IActionResult Insert([FromBody] VisaApplicationCost visaApplicationCost)
        {

            string data = vcostbao.insert(visaApplicationCost);
            if (data.Equals("1"))
            {
                return Ok("1 row inserted");
            }
            else
            {
                return NotFound("No rows inserted");
            }

        }


        [HttpGet]
        [Route("GetAll")]
        public IActionResult GetAll()
        {
            List<VisaApplicationCost> visaapplist = vcostbao.getAll();
            if (visaapplist.Count != 0)
            {
                return Ok(visaapplist);
            }
            else
            {
                return NotFound("no records ");
            }
        }
    }
}