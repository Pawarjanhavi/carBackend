using System.ComponentModel.DataAnnotations;

namespace Car2Go.DTOs
{
    public class UpdateReviewDto
    {
        //[Required(ErrorMessage = "Review is required")]
        public int Rating { get; set; }
        public string ReviewText { get; set; }
    }
}
