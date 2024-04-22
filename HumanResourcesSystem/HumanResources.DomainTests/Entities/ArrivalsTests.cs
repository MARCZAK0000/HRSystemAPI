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
            var result = new Arrivals();

            result.Arrival = DateTime.Now;
            result.Departure = DateTime.Now.AddHours(8);

            //act
            result.CompleteDay();

            result.IsCompleted.Should().BeTrue();
        }

    }
}