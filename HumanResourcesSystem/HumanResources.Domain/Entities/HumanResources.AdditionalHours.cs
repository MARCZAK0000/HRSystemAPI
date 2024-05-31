using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Entities
{
    public class AdditionalHours
    {
        public int AdditionalHoursID {  get; set; }    
        
        public UserInfo User { get; set; }
        
        public string Title { get; set; }

        public string UserID { get; set; }

        public string UserCode { get; set; }    

        public string SuperVisiorID { get; set; }

        public Supervisiors SuperVisior { get; set; }

        public DateTime CreatedDay { get; set; }

        public DateTime StartTime { get; set; } 

        public DateTime EndTime { get; set; } 

        public int Duration { get; set; }  

        public void CalculateAdditionalHours () 
        {
            Duration =  (EndTime-StartTime).Hours;
        }
    }
}
