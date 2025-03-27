using System.ComponentModel.DataAnnotations;

namespace Car2Go.DTOs
{
    public class UserDto
    {

        //[Required(ErrorMessage = "First name is requires")]
        public string FirstName { get; set; }

        //[Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }

        //[Required(ErrorMessage = "Email address is required")]
        //[EmailAddress(ErrorMessage = "Invalid Email address")]
        public string? Email { get; set; }


        //[Required(ErrorMessage = "Password is required.")]
        //[StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
        public string? Password { get; set; }

        //[Required(ErrorMessage = "Phone number required")]
        //[Phone(ErrorMessage = "Invalid Format")]
        public string? PhoneNumber { get; set; }

        //[Required(ErrorMessage = "User role required")]
        public List<string> RoleType { get; set; }

        public string? AccountStatus { get; set; }
    }
}
