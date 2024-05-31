namespace HumanResources.Domain.AdditionalHoursModel
{
    public class AdditionalHoursResponse
    {
        public string Title { get; set; }      

        public string UserCode { get; set; }    

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Duration { get; set; }
    }
}
