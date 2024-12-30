using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PVMS_Project.Models.ENTITIES
{
    [Table(name:"VisaApplicationCost_6")]
    public class VisaApplicationCost
    {
        
        [Key]
        public string Occupation { set; get; }
        
        public string Place { set; get; }
        public double Price { set; get; }
    }
}