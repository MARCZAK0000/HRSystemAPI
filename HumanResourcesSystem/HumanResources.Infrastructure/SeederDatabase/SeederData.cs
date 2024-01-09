using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Infrastructure.SeederDatabase
{
    public abstract class SeederData
    {
        protected List<string> DeparmentsList = new List<string>()
        {
            "HR", "Marketing", "Production", "Fiance", "Purchasing", "Sales", "Administration", "IT"
        };
    }
}
