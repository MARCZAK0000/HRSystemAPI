using HumanResources.Domain.Entities;

namespace HumanResources.API.Controllers
{
    public class EmployeePayHistory
    {
        public int EmployeePayHistoryID { get; set; }

        public UserInfo User { get; set; }

        public string UserID { get; set; }

        public int EmpolyeePayID { get; set; }

        public EmployeePay EmployeePay { get; set; }

        public decimal MonthPayment { get; set; }

        public decimal MonthPaymentEuro { get; set; }

        public decimal MonthPaymentUSD { get; set; }

        public DateTime CreateDays { get; set; }

        public DateTime ModifiededDate { get; set; }
    }
}
