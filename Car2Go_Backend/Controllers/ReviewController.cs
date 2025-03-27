using Car2Go.DTOs;
using Car2Go.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car2Go.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly IReviewService _reviewService;
        public ReviewController(IReviewService reviewService, ILog logger)
        {
            _reviewService = reviewService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        [Route("get-all-user-Reviews")]
        [HttpGet]
        public ActionResult<IEnumerable<ReviewOfUserDto>> GetAllUserReviews()
        {
            IEnumerable<ReviewOfUserDto> result = _reviewService.GetReviewsOfAllUsers();
            if (result.Count() <= 0) return NotFound();

            return Ok(result);
        }
        //*******************************************************************************************************************************************


        [Route("get-Review-user-car")]
        [HttpGet]
        public ActionResult<ReviewDto> GetReviewOfUser(string email,string licensePlate)
        {
            ReviewDto result = _reviewService.GetReviewOfCarWithUser(email,licensePlate);
            try
            {
                if (result == null)
                {
                    _logger.Error("User is not found");
                    return NotFound();

                }
                _logger.Info("No error found.");
            }
            catch (Exception ex)
            {
                _logger.Error("Error:" + ex.Message);
            }
            return Ok(result);
        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles = "Admin")]
        [Route("get-all-Reviews")]
        [HttpGet]
        public ActionResult<IEnumerable<ReviewDto>> GetAllReviews()
        {
            IEnumerable<ReviewDto> result = _reviewService.GetReviews();
            if (result.Count() <= 0) return NotFound();

            return Ok(result);
        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles = "User")]
        [Route("give-review")]
        [HttpPost]

        public ActionResult<ReviewDto> AddNewReview([FromBody] CreateReviewDto reviewDto, string email, string licensePlate)
        {
            if (reviewDto == null)
            {
                return BadRequest();
            }

           var newReviewCreate = _reviewService.CreateReview(reviewDto,email,licensePlate);

            if (newReviewCreate == null) { return NotFound("User or Car not Found"); }

            return Ok(newReviewCreate);

            //return CreatedAtRoute("GetAllReviews", reviewDto);
        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles = "User")]
        [Route("update-review")]
        [HttpPut]

        public IActionResult UpdateReviewDetails([FromBody] UpdateReviewDto updateReviewDto,string email, string licensePlate)
        {

            var result = _reviewService.UpdateReview(updateReviewDto, email,licensePlate);
            if (result == false)
            {
                return BadRequest();
            }
            return Ok(new {result=true});
        }

        //*******************************************************************************************************************************************

        [Authorize(Roles = "User")]
        [Route("delete-review")]
        [HttpDelete]

        public IActionResult DeleteLocationDetails(string email)
        {
            var result = _reviewService.DeleteReview(email);
            if (result == false) return NotFound();
            return NoContent();
        }
    }
}
