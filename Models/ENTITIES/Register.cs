using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PVMS_Project.Models.ENTITIES
{
    [Table(name:"Register_6")]
    public class Register
    {
        public string Firstname { set; get; }
        public string Surname { set; get; }
        public DateTime DateOfBirth { set; get; }
        public string Address { set; get; }
        public string Contactno { set; get; }
        public string Email { set; get; }
        public string Qualification { set; get; }
        public string Gender { set; get; }
        public string ApplyType { set; get; }
        public string HintQuestion { set; get; }
        public string HintAnswer { set; get; }
        public string? Status { set; get; }

        [Key]
        public string? UserId { set; get; }
        public string? Password { set; get; }
        public string? CitizenType { set; get; }
    }
}

