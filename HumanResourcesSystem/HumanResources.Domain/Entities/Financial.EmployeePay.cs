using HumanResources.API.Controllers;

namespace HumanResources.Domain.Entities
{
    public class EmployeePay
    {
        public int EmployeePayID {  get; set; } 

        public string UserID { get; set; }

        public UserInfo User { get; set; }

        public decimal RatePLN { get; set; }

        public decimal? RateEURO{ get; set; }   

        public decimal? RateUSD { get; set; }

        public DateTime ModifiedDate { get; set; }

        public List<EmployeePayHistory>? EmployeePayHistory { get; set; }
    }
}
