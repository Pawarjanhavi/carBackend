using System.ComponentModel.DataAnnotations;

namespace Car2Go.DTOs
{
    public class ReviewOfUserDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Model { get; set; }
        public string LicensePlate { get; set; }
        public string ReviewText { get; set; }
        public int Rating { get; set; }
        public string ReviewCreatedAt { get; set; }
    }
}
