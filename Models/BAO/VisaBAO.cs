using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using PVMS_Project.Data;
using PVMS_Project.Models.DAO;
using PVMS_Project.Models.ENTITIES;

namespace PVMS_Project.Models.BAO
{
    public class VisaBAO
    {
        private readonly PassportDBContext _context;
        VisaDAO visaDAO = null;

        public VisaBAO(PassportDBContext context)
        {
            _context = context;
            visaDAO = new VisaDAO(_context);
        }

        //method for get all the details of visa
        public List<Visa> getAll()
        {
            return visaDAO.getAll();
        }

        //method for get all the details of visa based on visa id
        public Visa getByVisaId(string visaid)
        {
            return _context.visas.Where(x => x.VisaId.Equals(visaid)).FirstOrDefault();

        }

        //method for get all the details of visa application cost table
        public List<VisaApplicationCost> getallVisaApp()
        {
            return _context.visaApplicationCosts.ToList();
        }
       
public string AlreadyCancelled(string visaId)
{
    // Fetch the visa record from the database
    Visa? visa = _context.visas
        .Where(x => x.VisaId.Equals(visaId))
        .FirstOrDefault();

    if (visa != null)
    {
        if (visa.Status.Equals("cancelled"))
         { 
                return "Visa already cancelled";

          }
          else
          {
            return "1";
          }
    }
    else
    {
        return "Visa not found.";
    }
    
}


        public string cancel(Visa visa, string visaId)
        {

          string s=AlreadyCancelled(visa.Status);
          if(s.Equals("1"))
          {
            return s;
          }
            var existingVisa = getByVisaId(visaId);
            if (existingVisa == null)
            {
                return "Visa not found";
            }
            if (existingVisa.ExpireDate == null)
            {
                return "Visa expiry date is not available";
            }
            if (existingVisa.ExpireDate < DateTime.Now)
            {
                return "Cannot cancel the visa as it has already expired";
            }
            var monthsUntilExpiry = ((DateTime)existingVisa.ExpireDate - DateTime.Now).Days / 30;
            var cancelCharge = calculateCancellationCharge(visa, monthsUntilExpiry);

            existingVisa.Status = "cancelled";
            _context.SaveChanges();
            return $"Your cancellation for the visa is successful. Please pay Rs: " + cancelCharge + ". Months To Expire: " + monthsUntilExpiry + ". Time To Expire: " + existingVisa.ExpireDate;
          
    

        }

        //Method for calculate visa Cancellation Charge
        private double? calculateCancellationCharge(Visa visa, long diffInMonths)
        {
            double chargePercentage = 0.0;
            var occupation = visa.Occupation.Trim().ToLower() ?? string.Empty;
            switch (occupation)
            {
                case "student":
                    chargePercentage = diffInMonths < 6 ? 0.5 : 0.10;
                    break;
                case "self employed":
                    chargePercentage = diffInMonths < 6 ? 0.15 : 0.25;
                    break;
                case "private employee":
                    if (diffInMonths < 6)
                    {
                        chargePercentage = 0.15;
                    }
                    else if (diffInMonths < 12)
                    {
                        chargePercentage = 0.25;
                    }
                    else
                    {
                        chargePercentage = 0.20;
                    }
                    break;
                case "government employee":
                    if (diffInMonths < 6)
                    {
                        chargePercentage = 0.12;
                    }
                    else if (diffInMonths < 12)
                    {
                        chargePercentage = 0.20;
                    }
                    else
                    {
                        chargePercentage = 0.25;
                    }
                    break;
                case "retired employee":
                    chargePercentage = diffInMonths < 6 ? 0.10 : 0.20;
                    break;
                default:
                    return 0.0;
            }


            double? cancellationCharge = visa.RegistrationCost > 0 ? visa.RegistrationCost * chargePercentage : 0.0;
            Console.WriteLine($"Charge Percentage: {chargePercentage}");
            Console.WriteLine($"Cancellation Charge: {cancellationCharge}");
            return cancellationCharge;
        }

        //Method for Date Validation
        public bool DateValidate(DateTime visaApplicationDate, string id)
        {
            Passport passport = _context.passports.Where(x => x.Passport_id.Equals(id)).FirstOrDefault();
            return visaApplicationDate >= passport.Issue_date && visaApplicationDate <= passport.Expiry_date;
        }

        //Method for No User
        public string noUser(string passportid)
        {
            Passport passport = _context.passports.Where(x => x.Passport_id.Equals(passportid)).FirstOrDefault();
            if (passport == null)
            {
                return "Already";

            }
            return "1";
        }

        //Method for Insert Data
        public string insert(Visa visa)
        {
            string s = noUser(visa.PassportId);
            if (s.Equals("1"))
            {
                bool valid = DateValidate(visa.DateOfApplication, visa.PassportId);
                if (valid)
                {
                    visa.Status = "Active";
                    string visaId = generateVisaId(visa.Occupation);
                    visa.VisaId = visaId;
                    DateTime issueDate = dateOfIssue(visa.DateOfApplication);
                    visa.IssueDate = issueDate;
                    double dt = calculateVisaDetails(visa);
                    visa.RegistrationCost = dt;
                    DateTime? expiryDate = getUpdatedPassportExpiryDate(visa);
                    if (expiryDate.HasValue)
                    {
                        visa.ExpireDate = expiredatevalidation(visa);
                    }
                    else
                    {
                        visa.ExpireDate = issueDate.AddYears(1);
                    }

                    return visaDAO.insert(visa);
                }

                else
                {
                    return "Passport already Expired";
                }
            }
            else
            {
                return "No user with this Passport Id : " + visa.PassportId;
            }
        }

        //Method for Date Of Issue
        public DateTime dateOfIssue(DateTime dateOfApplication)
        {
            return dateOfApplication.AddDays(10);
        }
        public DateTime? getUpdatedPassportExpiryDate(Visa visa)
        {
            var passport = _context.passports
                        .Where(passport => passport.Expiry_date <= visa.ExpireDate)
                        .OrderByDescending(passport => passport.Expiry_date)
                        .FirstOrDefault();
            if (passport == null)
            {
                return null;
            }
            return passport.Expiry_date;
        }

        //Method for Calculate Date Of Expiry
        public int calculateDateOfExpiry(Visa visa)
        {
            int validityYears;
            string occupation = visa.Occupation;

            if (occupation.Equals("Student"))
            {
                return validityYears = 2;
            }
            else if (occupation.Equals("Private Employee"))
            {
                return validityYears = 3;
            }
            else if (occupation.Equals("Government Employee"))
            {
                return validityYears = 4;
            }
            else if (occupation.Equals("Self Employed"))
            {
                return validityYears = 1;
            }
            else if (occupation.Equals("Retired Employee"))
            {
                return validityYears = 1;
            }
            else
            {
                return validityYears = 5;
            }

            return validityYears;
        }

        //Method for Expiredate Validation
        public DateTime expiredatevalidation(Visa visa)
        {
            int cnt = calculateDateOfExpiry(visa);
            DateTime ExpireDate = visa.DateOfApplication.AddYears(cnt);
            return ExpireDate;
        }

        //Method for Generate VisID
        public string generateVisaId(string occupation)
        {
            string prefix = occupation switch
            {
                "Student" => "STU",
                "Private Employee" => "PE",
                "Government Employee" => "GE",
                "Self Employed" => "SE",
                "Retired Employee" => "RE",
                _ => throw new ArgumentException("Invalid occupation type.")
            };
            var maxVisaId = _context
                .visas.Where(v => v.VisaId.StartsWith(prefix))
                .OrderByDescending(v => v.VisaId)
                .Select(v => v.VisaId)
                .FirstOrDefault();

            string newId =
                maxVisaId == null
                    ? "0001"
                    : (int.Parse(maxVisaId.Substring(prefix.Length + 1)) + 1).ToString("D4");

            return $"{prefix}-{newId}";
        }

        //Method for  Calculate Visa Details
        public double calculateVisaDetails(Visa visa)
        {
            string occupation = visa.Occupation;
            string country = visa.Country;

            if (visa == null)
            {
                throw new ArgumentNullException(nameof(visa), "Visa object cannot be null.");
            }

            double registrationCost = 0.00;

            if (occupation.Equals("Student"))
            {
                if (country.Equals("INDIA"))
                {
                    return registrationCost = 2500;
                }
                else if (country.Equals("USA"))
                {
                    return registrationCost = 3000;
                }
                else if (country.Equals("CHINA"))
                    return registrationCost = 1500;
                else
                    return registrationCost = 3500;
            }
            else if (occupation.Equals("Private Employee"))

            {
                if (country.Equals("INDIA"))
                {
                    return registrationCost = 2500;
                }
                else if (country.Equals("USA"))
                {
                    return registrationCost = 5000;
                }
                else if (country.Equals("CHINA"))
                    return registrationCost = 3000;
                else
                    return registrationCost = 4000;
            }
            else if (occupation.Equals("Government Employee"))
            {
                if (country.Equals("INDIA"))
                {
                    return registrationCost = 2500;
                }
                else if (country.Equals("USA"))
                {
                    return registrationCost = 6000;
                }
                else if (country.Equals("CHINA"))
                    return registrationCost = 3000;
                else
                    return registrationCost = 4500;
            }
            else if (occupation.Equals("Self Employed"))
            {
                if (country.Equals("INDIA"))
                {
                    return registrationCost = 2500;
                }
                else if (country.Equals("USA"))
                {
                    return registrationCost = 6000;
                }
                else if (country.Equals("CHINA"))
                    return registrationCost = 4000;
                else
                    return registrationCost = 9000;
            }
            else if (occupation.Equals("Retired Employee"))
            {
                if (country.Equals("INDIA"))
                {
                    return registrationCost = 2500;
                }
                else if (country.Equals("USA"))
                {
                    return registrationCost = 3000;
                }
                else if (country.Equals("CHINA"))
                    return registrationCost = 1500;
                else
                    return registrationCost = 3500;
            }
            else
            {
                return registrationCost;
            }
            return registrationCost;
        }
    }
}