using BikeRental.Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikeRental.API
{
    public class RentalCostCalculator
    {
        public decimal Calculate(Rental rental)
        {
            var timeDifference = ((DateTime) rental.RentalEnd).Subtract(rental.RentalBegin).TotalMinutes;

            if (timeDifference <= 15.0)
                return 0.0M;

            return rental.Bike.RentalPriceFirstHour + (Convert.ToDecimal(timeDifference / 60 - 60) * rental.Bike.RentalPriceAdditionalHour);
        }
    }
}
