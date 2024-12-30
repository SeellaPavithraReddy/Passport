using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVMS_Project.Data;
using PVMS_Project.Models.ENTITIES;

namespace PVMS_Project.Models.DAO
{
    public class VisaApplicationCostDAO
    {
        private readonly PassportDBContext _context;

        public VisaApplicationCostDAO(PassportDBContext context)
        {
            _context = context;
        }

        //Method for get all the details of visa application cost
        public List<VisaApplicationCost> getAll()
        {
            return _context.visaApplicationCosts.ToList();
        }

        //Method for insert data into visa application cost
        public string insertVisaApp(VisaApplicationCost visaApplicationCost)
        {
            _context.visaApplicationCosts.Add(visaApplicationCost);
            _context.SaveChanges();
            return "1";
        }

        //Method for get all the details of visa application cost based on visa id
        public Visa getByVisaId(string visaid)
        {
            Visa? visa = (Visa?)_context.visas.Where(x => x.VisaId == visaid).FirstOrDefault();
            return visa;
        }
    }
}