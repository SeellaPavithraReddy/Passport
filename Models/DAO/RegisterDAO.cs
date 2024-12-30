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
    public class RegisterDAO
    {
        private readonly PassportDBContext _context;

        public RegisterDAO(PassportDBContext context)
        {
            _context = context;
        }

        //Method To Get All the Details of Register
        public List<Register> getAll()
        {
            return _context.registers.ToList();
        }

        //Method To Insert the Details of Register
        public string insert(Register register)
        {
            try
            {
                string? pwd = register.Password;

                register.Password = BCrypt.Net.BCrypt.HashPassword(register.Password);

                register.Status = "N";
                _context.Entry(register).State = EntityState.Added;
                _context.SaveChanges();
                return $"User ID : {register.UserId}, Password : {pwd}, Citizen Type : {register.CitizenType}";
            }
            catch (DbUpdateException ex)
            {
                var sqlException = ex.GetBaseException() as SqlException;

                if (sqlException != null)
                {
                    if (sqlException.Message.Contains("PK_Register_6"))
                        return "UserId cannot be duplicate";
                    else if (sqlException.Message.Contains("IX_Register_6_Contactno"))
                        return "Contact Number Already Exists";
                    else if (sqlException.Message.Contains("IX_Register_6_Email"))
                        return " Email already Exists";
                    else
                        return "Database update error ";
                }
                else
                {
                    return "Server Not found";
                }
            }
        
            catch (Exception ex)
            {
                return "An error occurred: ";
            }
        }

    }
}


