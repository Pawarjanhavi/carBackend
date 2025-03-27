using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Car2Go.Models
{
    public class Review
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReviewId { get; set; }

        [Required(ErrorMessage = "Review is required")]
        public string ReviewText { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        public int Rating { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly ReviewCreatedAt { get; set; }

        public bool hasReview { get; set; }

        public int CarId { get; set; }
        public int UserId { get; set; }

        //navigation properties
        public User user { get; set; }
        public Car car { get; set; }
    }
}
