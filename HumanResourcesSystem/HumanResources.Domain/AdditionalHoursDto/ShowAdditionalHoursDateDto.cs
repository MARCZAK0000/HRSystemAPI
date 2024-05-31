namespace HumanResources.Domain.AdditionalHoursDto
{
    public class ShowAdditionalHoursDateDto
    {
        public string UserCode { get; set; }

        public DateTime From { get; set; }  

        public DateTime To { get; set; }

        public int PageSize { get; set; }

        public int PageNumber { get; set; }
    }
}
