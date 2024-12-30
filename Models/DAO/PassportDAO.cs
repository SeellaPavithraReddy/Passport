using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PVMS_Project.Data;
using PVMS_Project.Models.ENTITIES;

namespace PVMS_Project.Models.DAO
{
    public class PassportDAO
    {
        private readonly PassportDBContext _context;

        public PassportDAO(PassportDBContext context)
        {
            _context = context;
        }

        public List<Passport> getAll()
        {
            return _context.passports.ToList();
        }

        
        public string insert(Passport passport)
        {
            try
            {
                _context.Entry(passport).State = EntityState.Added;
                _context.SaveChanges();
                return "1";
            }
            catch (DbUpdateException ex)
            {

                var innerException = ex.InnerException?.Message;
                return $"Error: {innerException}";
            }
            catch (Exception ex)
            {
                return $"General error: {ex.Message}";
            }
        }
    }
}