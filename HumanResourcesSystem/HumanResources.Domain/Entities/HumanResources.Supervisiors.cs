using HumanResources.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Entities
{
    public class Supervisiors
    {
        public UserInfo User { get; set; }

        public string UserID { get; set; }

        public virtual Departments? Departments { get; set; }

        public int DepramentID { get; set; }
   
        public virtual List<AdditionalHours>? SubordinateAdditionalHours { get; set; }
    }
}
