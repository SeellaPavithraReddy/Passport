using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PVMS_Project.Models.ENTITIES
{
    [Table(name:"Visa_6")]
    public class Visa
    {
        [Key]
        public string? VisaId { set; get; }
        public string Country { set; get; }
        public DateTime DateOfApplication { set; get; }
        public DateTime? IssueDate { set; get; }
        public DateTime? ExpireDate { set; get; }       
         public string Occupation { set; get; }
        [ForeignKey("Occupation")]
        public VisaApplicationCost? visaCost{set; get;}
        public double? RegistrationCost { set; get; }
        public string? Status { set; get; }
        public string PassportId { set; get; }

        [ForeignKey("PassportId")]
        public Passport? passport { set; get; }
    }
}