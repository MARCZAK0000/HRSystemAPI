using HumanResources.Domain.Exceptions;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore.Query.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HumanResources.Domain.CalculateDays
{
    public class CalculateFactory
    {
        public CalculateBase CalculateDays(string countryName, CalculateDaysInfo initInfo)
        {
            switch(countryName) 
            {
                case "PL":
                    return new CalculateDays_PL(initInfo.InitialDays, initInfo.BonusDays, initInfo.RequirmentYears, initInfo.Level);
                    
                default:
                    throw new BadRequestException("Invalid name, check AppSetting");

            }
        }
    }
}
