namespace HumanResources.Domain.AdditionalHoursDto
{
    public class CreateAdditionalHoursRequestDto
    {
        public string Title { get; set; }

        public string UserCode { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }
    }
}
