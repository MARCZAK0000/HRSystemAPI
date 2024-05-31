namespace HumanResources.Domain.AdditionalHoursDto
{
    public class UpdateAdditionalHoursRequestDto
    {
        public int AdditionalHoursID { get; set; } 

        public string Title { get; set; }   

        public string UserCode { get; set; }    

        public DateTime StartTime { get; set; }   

        public DateTime EndTime { get; set; }

    }
}
