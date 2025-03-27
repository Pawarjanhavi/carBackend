using System.ComponentModel.DataAnnotations;

namespace Car2Go.DTOs
{
    public class LocationDto
    {
        [Required(ErrorMessage = "City is required")]
        public string City { get; set; }

        [Required(ErrorMessage = "Address required")]
        public string Address { get; set; }

        [Required(ErrorMessage = "State is required")]
        public string State { get; set; }

        [Required(ErrorMessage = "Country is required")]
        public string Country { get; set; }

        [Required]
        public string ZipCode { get; set; }
    }
}
