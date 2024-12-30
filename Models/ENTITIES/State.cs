using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PVMS_Project.Models.ENTITIES
{
    [Table(name:"State_6")]
    public class State
    {
        [Key]
        public int State_Id { get; set; }    
        public string StateName { get; set; }
    }
}