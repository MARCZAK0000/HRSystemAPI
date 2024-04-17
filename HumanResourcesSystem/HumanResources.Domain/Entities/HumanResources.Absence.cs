namespace HumanResources.Domain.Entities
{
    public class Absence
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AbsenceId { get; set; }  

        public AbsencesType AbsencesType { get; set; }

        public string UserId { get; set; }

        public UserInfo User { get; set; }

        public DateTime CreatedTime { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int PeriodOfTime { get;  private set; }

        public bool IsAccepted { get; set; } = false;

        public bool Declined { get; set; } = false;

        public void CalculatePeriodOfTime()
        {
            PeriodOfTime = EndTime.Subtract(StartTime).Days + 1;
        }

        public int CalculateDayToUse(Func<int> calculate) 
        {
            return calculate();
        }

    }
}
