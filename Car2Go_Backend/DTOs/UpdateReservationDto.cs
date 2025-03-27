using System.ComponentModel.DataAnnotations;

namespace Car2Go.DTOs
{
    public class UpdateReservationDto
    {
        [Required(ErrorMessage = "Pickup date is required")]
        [DataType(DataType.Date)]
        public DateOnly PickUpDate { get; set; }

        [Required(ErrorMessage = "Dropoff date is required")]
        [DataType(DataType.Date)]
        public DateOnly DropOffDate { get; set; }
    }
}
