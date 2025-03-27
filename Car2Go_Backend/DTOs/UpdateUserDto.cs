using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Car2Go.DTOs
{
    public class UpdateUserDto
    {

        [Required(ErrorMessage = "First name is requires")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [Phone(ErrorMessage = "Invalid Format")]
        public string? PhoneNumber { get; set; }

    }
}
