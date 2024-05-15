namespace HumanResources.Domain.Entities
{
    public class ExchangeRate
    {
        public int ID { get; set; }

        public string NameFrom { get; set; }

        public string NameTo { get; set; }

        public decimal Rate { get; set; }

    }
}
