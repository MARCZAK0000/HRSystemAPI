using HumanResources.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace HumanResources.Domain.Entities.Tests
{
    public class ArrivalsTests
    {
        [Fact()]
        public void IsCompletedDay_ShouldBeTrue()
        {
            //arrange

            var result = new Arrivals();

            result.Arrival = DateTime.Now;
            result.Departure = DateTime.Now.AddHours(9);

            //act
            result.CompleteDay();

            result.IsCompleted.Should().BeTrue();
        }


        [Fact()]
        public void IsCompletedDay_ShouldBeFalse()
        {
            var result = new Arrivals
            {
                Arrival = DateTime.Now,
                Departure = DateTime.Now.AddHours(7)
            };

            //act
            result.CompleteDay();

            result.IsCompleted.Should().BeFalse();
        }

        [Fact()]
        public void CalculationDuration_ShouldbeGretherOrEqualTo_8()
        {
            var result = new Arrivals
            {
                Arrival = DateTime.Now,
                Departure = DateTime.Now.AddHours(8)
            };

            result.CalculateDuration();

            result.Duration.Should().BeGreaterThanOrEqualTo(new TimeSpan(8, 0, 0));
        }

        [Fact()]
        public void CalculationDuration_ShouldbeLessThan_8()
        {
            var result = new Arrivals
            {
                Arrival = DateTime.Now,
                Departure = DateTime.Now.AddHours(7)
            };

            result.CalculateDuration();

            result.Duration.Should().BeLessThan(new TimeSpan(8, 0, 0));
        }
    }
}