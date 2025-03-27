using Car2Go.DTOs;
using Car2Go.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car2Go.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AgentController : ControllerBase
    {
        IAgentService _agentService;
        IReservationService _reservationService;
        public AgentController(IAgentService agentService,IReservationService reservationService) { 
            _agentService = agentService;
            _reservationService = reservationService;
        }

        //[Authorize(Roles = "Admin,User")]
        [Route("agent-get-all-cars")]
        [HttpGet]
        public ActionResult<IEnumerable<AgentCarWithImageDto>> GetAllCars()
        {
            IEnumerable<AgentCarWithImageDto> result = _agentService.GetCars();

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }
        //*******************************************************************************************************************************************

        //[Authorize(Roles = "Admin")]
        [Route("agent-Create-Car")]
        [HttpPost]
        public ActionResult<AgentCarDto> AddNewCar([FromForm] AgentCreateCarDto carDto,string email)
        {
            if (carDto.CarImageFile == null || carDto.CarImageFile.Length == 0)
            {
                return BadRequest("No image file uploaded");
            }

            long maxFileSize = 10485760;

            if (carDto.CarImageFile.Length > maxFileSize)
            {
                return BadRequest("Image file size Should be less than 10MB");
            }

            var result = _agentService.CreateCar(carDto,email);

            if (result.Model == null)
            {
                ModelState.AddModelError("CustomError", "Car already Exists!");
                return BadRequest();
            }
            if (carDto == null) return BadRequest("Car details should not be empty");

            return Ok(carDto);
            //return CreatedAtRoute("GetAllCars", carDto);
        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles = "Admin")]
        [Route("agent-update-car")]
        [HttpPut]
        public ActionResult UpdateCarDetails(AgentUpdateCarDto carDto, string licensePlate)
        {
            var result = _agentService.UpdateCar(carDto, licensePlate);
            if (result == false) return BadRequest();
            return Ok(result);
        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles = "Admin")]
        [Route("agent-update-car-with-location")]
        [HttpPut]
        public ActionResult UpdateCarAndLocation(AgentCarWithLocationDto carWithLocationDto, string licensePlate)
        {
            var result = _agentService.UpdateCarWithLocation(carWithLocationDto, licensePlate);
            if (result == false) return BadRequest();
            return Ok(result);
        }
        //*******************************************************************************************************************************************

        //[Authorize(Roles = "Admin")]
        [Route("agent-delete-car")]
        [HttpDelete]
        public ActionResult DeleteCarDetails(string licensePlate)
        {
            var result = _agentService.DeleteCar(licensePlate);

            if (result == false) return NotFound();

            return Ok(result);
        }

        [Route("Reserved-AgentCar")]
        [HttpGet]
        public ActionResult<List<AllDetailsReservationDto>> GetCommonReservationDetails()
        {
            // Get all agent cars
            var agentCars = _agentService.GetCars();

            // Get all reservation cars
            var reservationCars = _reservationService.GetAllReservation();

            // Check if either list is null
            if (agentCars == null || reservationCars == null)
            {
                return NotFound("No cars found in the system.");
            }

            // Find common reservation details using the helper method
            var commonReservations = reservationCars
                .Where(r => agentCars.Any(a => a.LicensePlate == r.CarNumber))
                .ToList();

            // If no common reservations are found, return NotFound
            if (commonReservations.Count == 0)
            {
                return NotFound("No matching reservation details found.");
            }

            // Return the list of matching reservation details
            return Ok(commonReservations);
        }



        [Route("GetCarsByEmail")]
        [HttpGet]
        public IActionResult GetCarsByEmail([FromQuery] string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                return BadRequest("Email is required.");
            }

            try
            {
                // Assuming you have a service for car logic
                List<AgentCarWithImageDto> cars = _agentService.GetCarsByEmail(email);

                if (cars == null || cars.Count == 0)
                {
                    return NotFound("No cars found for the provided email.");
                }

                return Ok(cars); // Return 200 OK with car data
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
