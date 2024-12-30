using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PVMS_Project.Models.ENTITIES
{
    [Table(name:"City_6")]
    public class City
    {

        [Key]
        public string Pincode { set; get; }
        public string City_name { set; get; }
        public int State_Id{set;get;}

        [ForeignKey("State_Id")]
        public State? state{ set; get; }
    }
}