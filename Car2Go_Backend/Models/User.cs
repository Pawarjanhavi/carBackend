using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car2Go.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required(ErrorMessage = "First name is requires")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        public string? LastName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email address")]
        public string? Email { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(20, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 20 characters.")]
        public string? Password { get; set; }

        [Required(ErrorMessage = "Phone number required")]
        [Phone(ErrorMessage = "Invalid Format")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "User role required")]
        public List<string> RoleType { get; set; }

        public Boolean IsDeleted {  get; set; }

        //navigation properties

        public ICollection<Review> reviews { get; set; }
        public ICollection<Reservation> reservations { get; set; }
    }
}
