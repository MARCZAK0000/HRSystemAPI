using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Entities
{
    public class Employee
    {
        public Guid Id { get; set; }

        public Departments Departments { get; set; }

        public int DepartmentID { get; set; }   

        public UserInfo User { get; set; }

        public int UserId {  get; set; }
    }
}
