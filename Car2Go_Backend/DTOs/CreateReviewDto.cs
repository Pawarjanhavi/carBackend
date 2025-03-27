using System.ComponentModel.DataAnnotations;

namespace Car2Go.DTOs
{
    public class CreateReviewDto
    {
        [Required(ErrorMessage = "Review is required")]
        public string ReviewText { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        public int Rating { get; set; }

    }
}
