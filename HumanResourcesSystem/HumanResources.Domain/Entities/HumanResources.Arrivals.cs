using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Entities
{
    public class Arrivals
    {
        public int Id { get; set; }

        public string UserCode { get; set; }   
        
        public virtual User User { get; set; }

        public string UserId { get; set; }

        public DateTime? Arrival {  get; set; }  

        public DateTime? Departure { get; set;}

        public bool IsCompleted { get; private set; } = false;

        public int TimeLimit { get; } = 8;

        public void CompleteDay()
        {
           
            var PeriodOfTime = Departure - Arrival;
            if( PeriodOfTime ==null || PeriodOfTime.Value.Hours<TimeLimit) 
            { 
                IsCompleted = false;
                return;
            }

            IsCompleted = true;
               
        }
    }
}
