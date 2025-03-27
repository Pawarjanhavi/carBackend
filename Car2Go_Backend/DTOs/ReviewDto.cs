using System.ComponentModel.DataAnnotations;

namespace Car2Go.DTOs
{
    public class ReviewDto
    {
        [Required(ErrorMessage = "Review is required")]
        public string ReviewText { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        public int Rating { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateOnly ReviewCreatedAt { get; set; }
        public bool hasReview { get; set; }

    }
}
