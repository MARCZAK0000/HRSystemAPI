using HumanResources.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.CurrencyData
{
    public class CurrencyFactory
    {
        public Currency Fetch(string currencyName)
        {
            switch(currencyName) 
            {
                case "USD":
                    return new USDCurrency();
                case "EUR":
                    return new EurCurrency();
                case "PLN":
                    return new PLNCurrency();
                default:
                    throw new BadRequestException("Something went wrong");
            }
        }
    }
}
