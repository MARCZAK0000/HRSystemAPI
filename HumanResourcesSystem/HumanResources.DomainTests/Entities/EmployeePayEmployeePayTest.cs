using Xunit;
using HumanResources.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace HumanResources.Domain.Entities.Tests
{
    public class EmployeePayEmployeePayTest
    {
        [Fact()]
        public void CalculateRateEmployeePayTest_ShouldBeOK()
        {
            var employePay = new EmployeePay()
            {
                RatePLN = 10,
            };

            employePay.RateEURO = employePay.CalculateRate((amount, rate) =>
            {
                return amount * rate;
            }, employePay.RatePLN, (decimal)2);

            employePay.RateEURO.Should().Be(20);
        }

        [Fact()]
        public void CalculateRateEmployeePayTest_ShouldBeNotOK()
        {
            var employePay = new EmployeePay()
            {
                RatePLN = 10,
            };

            employePay.RateEURO = employePay.CalculateRate((amount, rate) =>
            {
                return amount * rate;
            }, employePay.RatePLN, (decimal)2);

            employePay.RateEURO.Should().NotBe(10);
        }
    }
}