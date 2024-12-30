using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVMS_Project.Data;
using PVMS_Project.Models.DAO;
using PVMS_Project.Models.ENTITIES;

namespace PVMS_Project.Models.BAO
{
    public class PassportBAO
    {
        private readonly PassportDBContext _context;
        PassportDAO dao = null;

        public PassportBAO(PassportDBContext context)
        {
            _context = context;
            dao = new PassportDAO(_context);
        }

        //Method for Getting all the Passports
        public List<Passport> getAllPassport()
        {
            return _context.passports.ToList();
        }

        //Method for Getting all the States
        public List<State> getAllState()
        {
            return _context.states.ToList();
        }

        //Method for Getting all the States by StateId
       

        //Method for Getting all the Passports by PassportId
        public Passport getByPassportId(string passportid)
        {
            return _context.passports.Where(x => x.Passport_id.Equals(passportid)).FirstOrDefault();

        }

        //Method for Update the Passport
        public string update(Passport passport, string passportid)
        {

            var existingPassport = getByPassportId(passportid);
            if (existingPassport != null)
            {
                
                var oldExpireDate = existingPassport.Expiry_date;

                if (existingPassport.Expiry_date.HasValue)
                {
                    existingPassport.Expiry_date = existingPassport.Issue_date.AddYears(10);
                }
                else
                {
                    existingPassport.Expiry_date = DateTime.UtcNow.AddYears(10);
                }

                 existingPassport.Issue_date = DateTime.Today;
                _context.SaveChanges();

                return $"Passport updated. Previous Expiry Date was: {oldExpireDate}, New Expiry Date is: {existingPassport.Expiry_date}";
            }
            else
            {
                return null;
            }
        }

        //Method for Validate The Issue Date
        public string isIssueDateValid(DateTime issueDate, string userId)
        {

            Register register = _context.registers
                .Where(x => x.UserId.Equals(userId))
                .FirstOrDefault();

            if (register == null)
            {

                return "User not found.";
            }

            DateTime dateOfBirth = register.DateOfBirth;

            DateTime eighteenYearsAfterDateOfBirth = dateOfBirth.AddYears(18);

            if (issueDate > eighteenYearsAfterDateOfBirth)
            {
                return "1";
            }
            else
            {
                return "The issue date must be more than 18 years after the date of birth.";
            }
        }


        //Method for Get Passport Number By UserId

        public string AlreadyUserId(string userId)
        {
            var existingPassport = _context.passports
                .FirstOrDefault(p => p.User_id.Equals(userId));

            if (existingPassport != null)
            {
                return $"A passport has already been created for this user: {existingPassport.Passport_id}";
            }

            return null;
        }
        //Method for Insert The Data into Passport Table
        public string insert(Passport passport)

        {
            string existingPassportMessage = AlreadyUserId(passport.User_id);
            if (existingPassportMessage != null)
            {
                return existingPassportMessage;
            }

            string issueDateValidation = isIssueDateValid(passport.Issue_date, passport.User_id);
            if (issueDateValidation != "1")
            {
                return issueDateValidation;
            }
            string numberString = typeofservicesData(passport.Type_of_services);
            int number = int.Parse(numberString);
            passport.ApplicationCost = number;
            string bookletType = bookletTypeOfData(passport.Booklet_type);
            passport.Passport_id = bookletType;
            DateTime d = expireDateValidate(passport.Issue_date);
            passport.Status = "Active";
            passport.Expiry_date = d;
            return dao.insert(passport);
        }

        //Method for Validate the Expire Date
        public DateTime expireDateValidate(DateTime IssueDate)
        {
            return IssueDate.AddYears(10);
        }

        //Method To Generate Cost for Types Of Services
        public string typeofservicesData(string TypeOfServices)
        {
            string services = TypeOfServices.Equals("Genral") ? "3000" : "5000";
            return services;
        }

        //Method for Generate Booklet Type
        public string bookletTypeOfData(string bookletType)
        {
            string before = bookletType.Equals("30Pages") ? "FPS-30" : "FPS-60";
            string? existing = _context.passports.
           Where(x => x.Passport_id.StartsWith(before))
           .OrderByDescending(u => u.Passport_id)
           .Select(x => x.Passport_id)
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
    }
}