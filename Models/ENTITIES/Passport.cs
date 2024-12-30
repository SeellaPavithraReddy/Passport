using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PVMS_Project.Models.ENTITIES
{
    [Table(name:"Passport_6")]
    public class Passport
    {
        public string User_id { set; get; }
        
        [ForeignKey("User_id")]
        public Register? register{ set; get; }
        [Key]
        public string? Passport_id { set; get; }
        public string Booklet_type { set; get; }
        public string City { set; get; }
        public string Country { set; get; }
        public DateTime Issue_date { set; get; }
        public DateTime? Expiry_date { set; get; }
        public string Pincode { set; get; }
        public string StateName { set; get; }
       
        public string? Status { set; get; }
        public string Type_of_services { set; get; }

        public int? ApplicationCost{set; get;}

    }
}