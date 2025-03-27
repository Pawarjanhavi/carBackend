﻿using Car2Go.Models;
using System.ComponentModel.DataAnnotations;

namespace Car2Go.DTOs
{
    public class AllDetailsReservationDto
    {
        public string ReservationStatus {  get; set; }

        [Required(ErrorMessage = "Pickup date is required")]
        [DataType(DataType.Date)]
        public DateOnly PickUpDate { get; set; }

        [Required(ErrorMessage = "Dropoff date is required")]
        [DataType(DataType.Date)]
        public DateOnly DropOffDate { get; set; }

        [Required]
        public string UserEmail { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CarMake { get; set; }
        public string CarModel { get; set; }
        public string CarNumber { get; set; }
        public string CarColour {  get; set; }
        public int TotalSeats { get; set; }
        public decimal TotalAmount { get; set; }
        public string ImageUrl { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }


    }
}
