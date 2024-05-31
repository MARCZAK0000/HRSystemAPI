using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace HumanResources.Domain.AdditionalHoursDto
{
    public class ShowAdditionalHoursDto
    {
        public string UserCode {  get; set; }   
        
        public int AdditionalHoursID { get; set; }
    }
}
