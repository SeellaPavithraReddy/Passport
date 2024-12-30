using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PVMS_Project.Data;

namespace PVMS_Project.Models.DAO
{
    public class LoginDAO
    {
        private readonly PassportDBContext _context;

        public LoginDAO(PassportDBContext context)
        {
            _context = context;
        }

        public string? validateUser(string userId, string password)
        {
            var user = _context.registers.SingleOrDefault(r => r.UserId == userId);
            if (user == null)
            {
                return "User Not Found";
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(password, user.Password);
            if (isPasswordValid)
            {
                return "valid," + user.Status;
            }
            else
            {
                return "Invalid Credentials";
            }
        }
    }
}