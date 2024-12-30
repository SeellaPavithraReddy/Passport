using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PVMS_Project.Models.ENTITIES;

namespace PVMS_Project.Models.DAO
{
    public interface IRegisterDAO
    {
        string insert(Register register);
        public List<Register> getall();
    }
}