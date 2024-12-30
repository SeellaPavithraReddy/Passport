using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using PVMS_Project.Data;
using PVMS_Project.Models.ENTITIES;

namespace PVMS_Project.Models.DAO
{
    public class VisaDAO
    {
        private readonly PassportDBContext _context;

        public VisaDAO(PassportDBContext context)
        {
            _context = context;
        }

        //Method for get all the visas
        public List<Visa> getAll()
        {
            return _context.visas.ToList();
        }

        //Method for insert data into visa table
        public string insert(Visa visa)
        {
            try
            {
                _context.Entry(visa).State = EntityState.Added;
                _context.SaveChanges();
                return "Applied Successfully"+visa.VisaId;
            }
            catch (NullReferenceException N)
            {
                return "NullReference";
            }
            catch (SqlException s)
            {
                return "Server Exception";
            }
            catch (Exception ex)
            {
                return "Error Occured";
            }
        }
    }
}