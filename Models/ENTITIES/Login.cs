using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PVMS_Project.Models.ENTITIES
{
    [Table(name:"Login_6")]
    public class Login
    {
        public string UserId { set; get; }
        public string Password { set; get; }
    }
}