using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PVMS_Project.Models.ENTITIES
{
     [Table(name:"Cost_6")]
    public class Cost
    {
        [Key]
        public int Id { set; get; }
        public int? Months { set; get; }
        public string Occupation { set; get; }
        public double Price { set; get; }
    }
}