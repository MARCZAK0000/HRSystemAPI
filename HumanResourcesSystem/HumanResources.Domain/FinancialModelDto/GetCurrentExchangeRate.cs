using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HumanResources.Domain.FinancialModelDto
{
    public class GetCurrentExchangeRate
    {
        public string Name { get; set; }    

        public decimal Value { get; set; }
    }
}
