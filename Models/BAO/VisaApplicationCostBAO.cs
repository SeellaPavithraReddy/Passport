using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVMS_Project.Data;
using PVMS_Project.Models.DAO;
using PVMS_Project.Models.ENTITIES;

namespace PVMS_Project.Models.BAO
{
    public class VisaApplicationCostBAO
    {
        private readonly PassportDBContext _context;

        VisaApplicationCostDAO Vcostdao = null;
        public VisaApplicationCostBAO(PassportDBContext context)
        {
            _context = context;
            Vcostdao = new VisaApplicationCostDAO(_context);
        }

         public List<VisaApplicationCost> getAll()
        {
            return Vcostdao.getAll();
        }
        public string insert(VisaApplicationCost visaApplicationCost)
        {
                _context.visaApplicationCosts.Add(visaApplicationCost);
                _context.SaveChanges();
                return "1";
        }

         public Visa getByVisaId(string visaid)
        {
            Visa? visa = (Visa?)_context.visas.Where(x => x.VisaId == visaid).FirstOrDefault();
            return visa;
        }
    }
}