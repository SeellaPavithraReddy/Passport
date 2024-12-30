using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVMS_Project.Data;
using PVMS_Project.Models.DAO;
using PVMS_Project.Models.ENTITIES;

namespace PVMS_Project.Models.BAO
{
    public class RegisterBAO
    {
        private readonly PassportDBContext _context;
        RegisterDAO registerDAO = null;

        public RegisterBAO(PassportDBContext context)
        {
            _context = context;
            registerDAO = new RegisterDAO(_context);
        }

        //Method For Get all the Register Details
        public List<Register> getAll()
        {
            return registerDAO.getAll();
        }

        //Method For Get all the Register Details By UserId
        public Register getById(string id)
        {
            return _context.registers.Where(e => e.UserId.Equals(id)).FirstOrDefault();
        }

        //Method For ForgotPassword
        public string forgotPassword(string userId, string password)
        {
            Register? user = getById(userId);

            if (user == null)
            {
                return "User Not Found";
            }

            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            user.Password = hashedPassword;
            _context.SaveChanges();

            return "Password updated successfully";


        }
       

        public string insert(Register register)
        {
            string appType = registerUserId(register.ApplyType);
            register.UserId = appType;
            string pass = autoGeneratePassword();
            register.Password = pass;
            int age = agecalculate(register.DateOfBirth);
            register.CitizenType = citizenType(age);
            
            return registerDAO.insert(register);
        }

        //Method To Autogenerate the Userid
        public string registerUserId(string applyType)
        {
            string before = applyType.Equals("PassportApply") ? "PASS" : "VISA";
            string? existing = _context.registers.
            Where(x => x.UserId.StartsWith(before))
            .OrderByDescending(u => u.UserId)
            .Select(x => x.UserId)
            .FirstOrDefault();

            string newId;
            if (existing == null)
            {
                newId = before + "0001";
            }
            else
            {
                int lastnum = int.Parse(existing.Substring(before.Length));
                int newnum = lastnum + 1;
                newId = before + newnum.ToString("D4");
            }
            return newId;

        }

        //Method To Autogenerate the Password
        public string autoGeneratePassword()
        {
            Random rnd = new Random();
            DateTime now = DateTime.Now;
            int day = now.Day;
            string dayString = day.ToString("D2");
            string month = DateTime.Now.ToString("MMM").ToUpper();
            char specialChar = getRandomSpecialCharacter();
            int number = rnd.Next(100, 1000);
            string numberString = number.ToString("D3");
            string password = $"{dayString}{month}{specialChar}{numberString}";
            return password;
        }

        //Method To Get Special Characters for Generate Password
        private char getRandomSpecialCharacter()
        {
            string specialChars = "!$*+";
            Random rnd = new Random();
            int index = rnd.Next(specialChars.Length);
            return specialChars[index];
        }

        //Method To calculate the age of citizen
        public int agecalculate(DateTime dateofbirth)
        {
            var today = DateTime.Now;
            var age = today.Year - dateofbirth.Year;
            if (dateofbirth.Date > today.AddYears(-age)) age--;
            return age;
        }

        //Method To Autogenerate Citizen Type Based on age
        public string citizenType(int age)
        {
            if (age >= 0 && age <= 1)
                return "Infant";
            else if (age > 1 && age <= 10)
                return "children";
            else if (age > 10 && age < 20)
                return "Teen";
            else if (age > 20 && age < 50)
                return "adult";
            else return "senior citizen";
        }

    }
}