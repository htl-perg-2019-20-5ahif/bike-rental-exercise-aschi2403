using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeRental.Library
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public Gender Gender { get; set; }
        [Required] [MaxLength(50)]
        public string FirstName { get; set; }
        [Required] [MaxLength(75)]
        public string LastName { get; set; }
        [Required]
        public DateTime Birthday { get; set; }
        [Required][MaxLength(75)]
        public String Street { get; set; }
        [MaxLength(10)]
        public string HouseNumber { get; set; }
        [Required][MaxLength(10)]
        public string ZipCode { get; set; }
        [Required][MaxLength(75)]
        public string Town { get; set; }
    }

    public enum Gender
    {
        Male,
        Female,
        Unknown
    }

    public class Bike
    {
        public int BikeId { get; set; }
        [Required][MaxLength(25)]
        public string Brand { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [MaxLength(1000)]
        public string Notes { get; set; }
        public DateTime DateOfLastService { get; set; }
        [Required]
        public Decimal RentalPriceFirstHour { get; set; }
        public Decimal RentalPriceAdditionalHour { get; set; }

        public BikeCategory BikeCategory { get; set; }

    }

    public enum BikeCategory
    {
        StandardBike,
        MountainBike,
        TreckingBike,
        RacingBike
    }

    public class Rental
    {
        public int RentalId { get; set; }
        [Required]
        public Customer Customer { get; set; }
        [Required]
        public Bike Bike { get; set; }
        [Required]
        public DateTime RentalBegin { get; set; }
        [Required]
        public DateTime RentalEnd { get; set; }
        [Required]
        public bool Paid { get; set; }
    }
}
