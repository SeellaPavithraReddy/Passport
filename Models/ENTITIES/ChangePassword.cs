using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PVMS_Project.Models.ENTITIES
{
    public class ChangePassword
    {
        public string User_id { set; get; }
        public string newPassword { set; get; }

        public string confirmPassword { set; get; }
    }
}
