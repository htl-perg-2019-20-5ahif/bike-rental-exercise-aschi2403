using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace BikeRental.Library
{
    public class Customer
    {
        public int CustomerId { get; set; }
        //public Gender Gender { get; set; }
        public string Gender { get; set; }
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

        public int RentalId { get; set; }
        public List<Rental> Rentals { get; set; }

        public Customer()
        {
            this.Rentals = new List<Rental>();
        }
    }

    public enum Gender
    {
        Male,
        Female,
        Unknown
    }

    public class Bike
    {
        public Bike()
        {
            this.Notes = "";
        }

        public int BikeId { get; set; }
        [Required][MaxLength(25)]
        public string Brand { get; set; }
        [Required]
        public DateTime PurchaseDate { get; set; }
        [MaxLength(1000)]
        public string Notes { get; set; }
        public DateTime DateOfLastService { get; set; }
        [Required]
        [DataType("decimal(18,2)")]
        public Decimal RentalPriceFirstHour { get; set; }
        [DataType("decimal(18,2)")]
        public Decimal RentalPriceAdditionalHour { get; set; }

        public BikeCategory BikeCategory { get; set; }

        public bool CurrentlyRented { get; set; }
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
        public int CustomerId { get; set; }
        
        public Customer Customer { get; set; }
        
        [Required]
        public int BikeId { get; set; }
        //[Required]
        public Bike Bike { get; set; }
        [Required]
        public DateTime RentalBegin { get; set; }
        public DateTime? RentalEnd { get; set; }

        [DataType("decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        public bool Paid { get; set; }
    }
}
