using Car2Go.Data;
using Car2Go.DTOs;
using Car2Go.Models;
using Car2Go.Services;
using Microsoft.EntityFrameworkCore;

namespace Car2Go.Repository
{
    public class ReviewRepository:IReviewService
    {
        Car2GoDBContext _db;
        public ReviewRepository(Car2GoDBContext db) { _db = db; }

        public ReviewDto CreateReview(CreateReviewDto reviewDto,string email,string licensePlate)
        {
            var userPresent = _db.Users.FirstOrDefault(c => c.Email.Replace(" ", "").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());
            var carPresent = _db.Cars.FirstOrDefault(u => u.LicensePlate.Replace(" ", "").ToLower().Trim() == licensePlate.Replace(" ", "").ToLower().Trim());


            try
            {
                if(userPresent == null) {
                    return new()
                    {
                        ReviewText = null,
                        Rating = 0,
                        hasReview =false
                    }; 
                }
                if (carPresent == null) {
                    return new()
                    {
                        ReviewText = null,
                        Rating = 0,
                        hasReview = false
                    };
                }

                Review newReview = new()
                {
                    ReviewText = reviewDto.ReviewText,
                    Rating = reviewDto.Rating,
                    ReviewCreatedAt = DateOnly.FromDateTime(DateTime.Now.Date),
                    UserId = userPresent.UserId,
                    CarId = carPresent.CarId,
                    hasReview = true,   
                };

                ReviewDto result = new ReviewDto()
                {
                    ReviewText = reviewDto.ReviewText,
                    Rating = reviewDto.Rating,
                    ReviewCreatedAt = DateOnly.FromDateTime(DateTime.Now.Date),
                    hasReview = true
                };


                _db.Reviews.Add(newReview);
                _db.SaveChanges();
                return result;
            }
            catch (Exception ex) { throw ex; }
            
        }

        public ReviewDto GetReviewOfCarWithUser(string email,string licensePlate)
        {

            ReviewDto reviews = null;

            var existingUser = _db.Users.FirstOrDefault(u=>
            u.Email.Replace(" ","").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());
            var existingCar = _db.Cars.FirstOrDefault(c =>
            c.LicensePlate.Replace(" ", "").ToLower().Trim() == licensePlate.Replace(" ", "").ToLower().Trim());

            try
            {
                if(existingUser==null || existingCar == null)
                {
                    return reviews = null;
                }

                var existingReview = _db.Reviews.FirstOrDefault(r => r.UserId == existingUser.UserId && r.CarId == existingCar.CarId);

                if (existingReview != null)
                {
                    reviews = new ReviewDto
                    {
                        ReviewText = existingReview.ReviewText,
                        Rating = existingReview.Rating,
                        ReviewCreatedAt = existingReview.ReviewCreatedAt
                    };
                    return reviews;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return reviews;
        }

        public List<ReviewDto> GetReviews()
        {
            List<Review> reviews;
            List<ReviewDto> allReviews = new List<ReviewDto>();
            try
            {
                reviews = _db.Reviews.ToList();

                foreach (Review review in reviews)
                {
                    allReviews.Add(new ReviewDto()
                    {
                        ReviewText = review.ReviewText,
                        Rating = review.Rating,
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return allReviews;
        }

        public bool UpdateReview(UpdateReviewDto updateReviewDto, string email,string licensePlate)
        {
            var userPresent = _db.Users.FirstOrDefault(c =>
            c.Email.Replace(" ", "").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());
            var car = _db.Cars.FirstOrDefault(c => c.LicensePlate == licensePlate.Replace(" ", "").ToLower().Trim());

            var reviewOfUser = _db.Reviews.FirstOrDefault(r=>r.UserId == userPresent.UserId && r.CarId==car.CarId);

            try
            {
                if (reviewOfUser == null)
                {
                    return false;
                }

                reviewOfUser.Rating = updateReviewDto.Rating;
                reviewOfUser.ReviewText = updateReviewDto.ReviewText;
                reviewOfUser.ReviewCreatedAt = DateOnly.FromDateTime(DateTime.Now.Date);

                _db.Reviews.Update(reviewOfUser);
                _db.SaveChanges();
                return true;
            }
            catch (Exception e) { throw e; }
        }

        
        public bool DeleteReview(string email)
        {
            var existingReviewOfUser = _db.Users.FirstOrDefault(c => c.Email.Replace(" ", "").ToLower().Trim() == email.Replace(" ", "").ToLower().Trim());

            try
            {
                if (email == null || existingReviewOfUser == null) { return false; }


                var deleteReviewOfUser = _db.Reviews.FirstOrDefault(r => r.UserId == existingReviewOfUser.UserId);

                if (deleteReviewOfUser != null)
                {
                    _db.Reviews.Remove(deleteReviewOfUser);
                    _db.SaveChanges();
                    return true;
                }
                else
                {
                    return false;
                }
            }catch (Exception e) { throw e; }
        }

        public List<ReviewOfUserDto> GetReviewsOfAllUsers()
        {
            List<Review> reviews;
            List<ReviewOfUserDto> allReviews = new List<ReviewOfUserDto>();
            try
            {
                reviews = _db.Reviews.ToList();

                foreach (Review review in reviews)
                {
                    var user = _db.Users.FirstOrDefault(u => u.UserId == review.UserId);
                    var car = _db.Cars.FirstOrDefault(c => c.CarId == review.CarId);


                    allReviews.Add(new ReviewOfUserDto()
                    {
                        FirstName=user.FirstName,
                        LastName=user.LastName,
                        Email= user.Email,
                        Model=car.Model,
                        LicensePlate=car.LicensePlate,
                        ReviewText = review.ReviewText,
                        Rating = review.Rating,
                        ReviewCreatedAt = (review.ReviewCreatedAt).ToString()
                    });
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return allReviews;
        }
    }
}
