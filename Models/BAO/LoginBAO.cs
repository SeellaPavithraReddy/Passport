using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVMS_Project.Data;
using PVMS_Project.Models.DAO;

namespace PVMS_Project.Models.BAO
{
    public class LoginBAO
    {
        private readonly PassportDBContext _context;
         LoginDAO loginDAO=null;

        public LoginBAO(PassportDBContext context)
        {
            this._context = context;
            loginDAO = new LoginDAO(_context);
        }

        //Method to Validate The User
        public string? validateUser(string userId,string password)
        {
            return loginDAO.validateUser(userId,password);
        }
    }
}