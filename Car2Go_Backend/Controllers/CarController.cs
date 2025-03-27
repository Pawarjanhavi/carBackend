using Car2Go.DTOs;
using Car2Go.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Car2Go.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarController : ControllerBase
    {
        ICarService _carService;
        public CarController(ICarService carService) { _carService = carService; }

        [Route("get-all-cars-with-rating")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithRatingsDto>> GetAllCarsWithRatings()
        {
            IEnumerable<CarWithRatingsDto> result = _carService.GetCarsWithRating();

            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles = "Admin,User")]
        [Route("get-all-cars")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithImageDto>> GetAllCars()
        {
            IEnumerable<CarWithImageDto> result = _carService.GetCars();

            if(result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles = "Admin,Agent")]
        [Route("Create-Car")]
        [HttpPost]
        public ActionResult<CarDto> AddNewCar([FromForm] CreateCarDto carDto)
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

            var result = _carService.CreateCar(carDto);

            if (result.Model==null)
            {
                ModelState.AddModelError("CustomError", "Car already Exists!");
                return BadRequest();
            }
            if (carDto == null) return BadRequest("Car details should not be empty");

            return Ok(carDto);
            //return CreatedAtRoute("GetAllCars", carDto);
        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles = "Admin,Agent")]
        [Route("update-car")]
        [HttpPut]
        public ActionResult UpdateCarDetails(UpdateCarDto carDto,string licensePlate)
        {
            var result = _carService.UpdateCar(carDto, licensePlate);
            if(result==false) return BadRequest();
            return Ok(result);
        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles = "Admin")]
        [Route("update-car-with-location")]
        [HttpPut]
        public ActionResult UpdateCarAndLocation(CarWithLocationDto carWithLocationDto,string licensePlate)
        {
            var result = _carService.UpdateCarWithLocation(carWithLocationDto, licensePlate);
            if (result == false) return BadRequest();
            return Ok(result);
        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles = "Admin")]
        [Route("delete-car")]
        [HttpDelete]
        public ActionResult DeleteCarDetails(string licensePlate)
        {
            var result = _carService.DeleteCar(licensePlate);

            if (result == false) return NotFound();

            return Ok(result);
        }

    }
}
