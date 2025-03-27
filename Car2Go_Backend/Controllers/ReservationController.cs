using Car2Go.DTOs;
using Car2Go.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car2Go.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        //[Authorize(Roles = "User")]
        [Route("reserve-car")]
        [HttpPost]
        public IActionResult ReserveCar([FromBody] ReservationRequestDto reservationRequestDto)
        {
            try
            {
                // Call the service to create the reservation and fetch the ReservationId
                int reservationId = _reservationService.CreateReservation(reservationRequestDto);

                // Return success response with ReservationId
                //return Ok(new
                //{
                //    Message = "Reservation created successfully.",
                //    ReservationId = reservationId
                //});
                return Ok (reservationId);
            }
            catch (Exception ex)
            {
                // Return error response in case of failure
                return BadRequest(new
                {
                    Message = "Error during reservation.",
                    Details = ex.Message
                });
            }
        }

        //public IActionResult ReserveCar([FromBody] ReservationRequestDto reservationRequestDto)
        //{
        //    try
        //    {
        //        // Set pickUpDate to today's date if it's null
        //        //pickUpDate = pickUpDate ?? DateOnly.FromDateTime(DateTime.Now.Date);


        //        // Set dropOffDate to pickUpDate + 1 day if it's null
        //        //dropOffDate = dropOffDate ?? pickUpDate.Value.AddDays(1);

        //        // Create the reservation object
        //        //var reservationRequest = new ReservationRequestDto
        //        //{
        //        //    Email = email,
        //        //    licensePlate = licensePlate,
        //        //    PickUpDate = pickUpDate,
        //        //    DropOffDate = dropOffDate
        //        //};

        //        // Call the service to create the reservation
        //        var result = _reservationService.CreateReservation(reservationRequestDto);

        //        if (result == "Reservation created successfully.")
        //        {
        //            return Ok(new { Message = result });
        //        }

        //        return BadRequest(new { Message = result });
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new { Message = "Error during reservation", Details = ex.Message });
        //    }
        //}

        //[Authorize(Roles = "User")]
        [Route("update")]
        [HttpPut]
        public IActionResult UpdateReservation([FromBody]UpdateReservationDto updateReservation,string email,string licensePlate)
        {
            if (string.IsNullOrEmpty(email) || updateReservation.PickUpDate == null || updateReservation.DropOffDate == null)
            {
                return BadRequest("Missing required data.");
            }

            // Call the service to create the reservation
            var result = _reservationService.UpdateReservation(updateReservation,email,licensePlate);

            // Return the result based on the service response
            if (result == "Reservation updated successfully.")
            {
                return Ok(new { Message = result });
            }
            else
            {
                return BadRequest(new { Message = result });
            }
        }

        //[Authorize(Roles = "Admin,User")]
        [Route("get-reservation-details-of-user-with-car")]
        [HttpGet]
        public IActionResult GetReservationDetailsOfUserWithAllCar(string email,string licensePlate)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }
            if (string.IsNullOrEmpty(licensePlate))
            {
                return BadRequest("Email is required.");
            }

            try
            {
                var reservationDetails = _reservationService.GetReservationDetailsOfUserWithCar(email,licensePlate);
                return Ok(reservationDetails);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-reservation-history-of-user")]

        [HttpGet]
        public ActionResult<IEnumerable<ReservationDetailsDto>> GetReservationHistoryOfUser(string email)

        {
            IEnumerable<ReservationDetailsDto> result = _reservationService.GetReservationHistoryOfUser(email);
            try
            {
                if (result == null)
                {
                    return NotFound();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }

            //if (string.IsNullOrEmpty(email))
            //{
            //    return BadRequest("Email is required.");
            //}    
            //try
            //{
            //    var reservationDetails = _reservationService.GetReservationHistoryOfUser(email);
            //    return Ok(reservationDetails);
            //}
           
        }

        [Authorize(Roles = "Admin,Agent")]
        [Route("get-all-reservation-details")]

        [HttpGet]
        public ActionResult<IEnumerable<AllDetailsReservationDto>> GetAllReservationDetails()
        {
           
            try
            {
                IEnumerable<AllDetailsReservationDto> reservationDetails = _reservationService.GetAllReservation();
                if(reservationDetails == null)
                {
                    return BadRequest("reservations not found");

                }
                return Ok(reservationDetails);
            }
            catch (Exception ex)
            {
                return BadRequest($"Error: {ex.Message}");
            }
        }

        //[Authorize(Roles = "Admin")]
        [HttpDelete("delete")]
        public IActionResult DeleteReservation([FromQuery] string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required.");
            }

            var result = _reservationService.DeleteReservation(email); // Pass email string to the service method

            if (result == "Reservation deleted successfully.")
            {
                return Ok(result);
            }

            return BadRequest(result); // Return error message in case of failure
        }

        //[Authorize(Roles = "User")]
        [HttpPost("Cancel")]
        public IActionResult CancelReservation(string email,string licensePlate)
        {
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email is required to cancel a reservation.");
            }

            try
            {
                // Call the service to cancel the reservation
                var result = _reservationService.CancelReservation(email,licensePlate);

                if (result.Contains("successfully"))
                {
                    return Ok(new { message = result });
                }
                else
                {
                    return NotFound(new { message = result });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while canceling the reservation.", details = ex.Message });
            }
        }
    }
}
