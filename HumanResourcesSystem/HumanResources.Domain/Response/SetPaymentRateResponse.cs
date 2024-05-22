using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.Response
{
    public class SetPaymentRateResponse
    {
        public string UserCode { get; set; }

        public decimal RatePLN { get; set; }

        public decimal? RateEuro { get; set; }   

        public decimal? RateUSD { get; set; }

    }
}
