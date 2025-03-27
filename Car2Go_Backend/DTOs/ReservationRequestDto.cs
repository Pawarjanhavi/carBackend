using System.ComponentModel.DataAnnotations;

namespace Car2Go.DTOs
{
    public class ReservationRequestDto
    {
        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string Email { get; set; } // User's email for identifying the reservation

        [Required(ErrorMessage = "License Plate required")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = "License Number should be exactly 10 characters long")]
        public string LicensePlate { get; set; }  //  To identify the reservation's car

        [Required(ErrorMessage = "Pickup date is required")]
        [DataType(DataType.Date)]
        public DateOnly PickUpDate { get; set; }

        [Required(ErrorMessage = "Dropoff date is required")]
        [DataType(DataType.Date)]
        public DateOnly DropOffDate { get; set; }
    }
}
