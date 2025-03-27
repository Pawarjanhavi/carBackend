using Car2Go.DTOs;

namespace Car2Go.Services
{
    public interface IReviewService
    {
        public List<ReviewDto> GetReviews();
        public ReviewDto  GetReviewOfCarWithUser(string email,string licensePlate);
        public ReviewDto CreateReview(CreateReviewDto reviewDto, string email, string licensePlate);
        public bool UpdateReview(UpdateReviewDto updateReviewDto, string email, string licensePlate);
        public bool DeleteReview(string email);
        public List<ReviewOfUserDto> GetReviewsOfAllUsers();
    }
}
