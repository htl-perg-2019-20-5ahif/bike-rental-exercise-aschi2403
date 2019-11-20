using BikeRental.API;
using BikeRental.Library;
using System;
using Xunit;

namespace XUnitTestProject1
{
    public class UnitTest1
    {
        [Fact]
        public void RentalsShorterThan15MinutesAreFree()
        {
            var rentalCostCalculator = new RentalCostCalculator();
            var rental = new Rental
            {
                RentalBegin = new DateTime(2019, 11, 20, 10, 20, 0),
                RentalEnd = new DateTime(2019, 11, 20, 10, 30, 0),
                Bike = new Bike
                {
                    RentalPriceFirstHour = 3,
                    RentalPriceAdditionalHour = 5
                }
            };
            var expectedCosts = 0;

            var costs = rentalCostCalculator.Calculate(rental);

            Assert.True(costs == expectedCosts, "Rental shorter than 15 Minutes should be free");

        }

        [Fact]
        public void SimpleRental()
        {
            var rentalCostCalculator = new RentalCostCalculator();
            var rental = new Rental
            {
                RentalBegin = new DateTime(2019, 11, 20, 5, 20, 0),
                RentalEnd = new DateTime(2019, 11, 20, 7, 20, 0),
                Bike = new Bike
                {
                    RentalPriceFirstHour = 3,
                    RentalPriceAdditionalHour = 5
                }
            };
            var expectedCosts = 8;

            var costs = rentalCostCalculator.Calculate(rental);

            Assert.True(costs == expectedCosts, "Result doesn't match expected costs");
        }
    }
}
