using System.ComponentModel.DataAnnotations;

namespace Car2Go.DTOs
{
    public class FilterCarDto
    {
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Color required")]
        public string Colour { get; set; }

        [Required(ErrorMessage = "Year required")]
        public int year { get; set; }

        [Required(ErrorMessage = "Car Seats required")]
        [Range(5, 7)]
        public int TotalSeats { get; set; }
        [Required]
        public bool AvailableStatus { get; set; }

        public decimal minPrice {  get; set; }
        public decimal maxPrice { get; set; }

    }
}
