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
    public class CarFilterController : ControllerBase
    {
        private readonly ICarFilterService _carFilterService;
        private readonly ILog _logger;
        public CarFilterController(ICarFilterService carFilterService, ILog logger)
        {
            _carFilterService = carFilterService;
            _logger = logger;
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-make-and-model")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByMakeAndModel(string make,string model)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByModelAndMake(make,model);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with make:{make} and model:{model} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-make-and-colour")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByMakeAndColour(string make, string colour)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByMakeAndColour(make, colour);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with make:{make} and colour:{colour} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-make-and-price")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByMakeAndPrice(string make, decimal price)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByMakeAndPrice(make, price);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with make:{make} and price:{price} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-make-and-seat")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByMakeAndSeats(string make, int seats)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByMakeAndSeats(make, seats);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with make:{make} and seats:{seats} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-make-and-availableStatus")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByMakeAndAvailability(string make, bool availableStatus)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByMakeAndAvailability(make, availableStatus);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with make:{make} and available staus:{availableStatus} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-make-and-availableDate")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByMakeAndAvailableDates(string make, DateOnly availableDate)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByMakeAndAvailableDates(make, availableDate);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with make:{make} and available date:{availableDate} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        //****************************************************************************************************************************************

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-model-and-price")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByModelAndPrice(string model, decimal price)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByModelAndPrice(model, price);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with model:{model} and price:{price} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-model-and-seat")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByModelAndSeats(string model, int seats)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByModelAndSeats(model, seats);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with model:{model} and seats:{seats} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-model-and-availability")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByModelAndAvailability(string model, bool availableStatus)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByModelAndAvailability(model, availableStatus);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with model:{model} and availability:{availableStatus} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-model-and-availableDate")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByModelAndAvailableDates(string model, DateOnly availableDate)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByModelAndAvailableDates(model, availableDate);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with model:{model} and available date:{availableDate} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        //*****************************************************************************************************************************************
        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-colour-and-price")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetCarsByColourAndPrice(string colour, decimal price)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByColourAndPrice(colour, price);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with colour:{colour} and Price:{price} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-colour-and-seats")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetCarsByColourAndSeats(string colour, int seats)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByColourAndSeats(colour, seats);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with colour:{colour} and seats:{seats} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-colour-and-availability")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetCarsByColourAndAvailabilty(string colour, bool availableStatus)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByColourAndAvailabilty(colour, availableStatus);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with colour:{colour} and availability:{availableStatus} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-colour-and-availableDate")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByColourAndAvailableDates(string colour, DateOnly availableDate)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByColourAndAvailableDates(colour, availableDate);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with colour:{colour} and available date:{availableDate} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        //*****************************************************************************************************************************************
        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-price-and-seats")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetCarsByPriceAndSeats(decimal price, int seats)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByPriceAndSeats(price, seats);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with price:{price} and seats:{seats} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-price-and-availability")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetCarsByPriceAndAvailability(decimal price, bool availableStatus)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByPriceAndAvailability(price, availableStatus);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with price:{price} and availability: {availableStatus} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-price-and-availableDate")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetCarsByPriceAndAvailableDates(decimal price, DateOnly availableDate)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByPriceAndAvailableDates(price, availableDate);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with price:{price} and available date: {availableDate} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        //*****************************************************************************************************************************************
        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-seats-and-availability")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetCarsBySeatsAndAvailability(int seats, bool availableStatus)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsBySeatsAndAvailability(seats,availableStatus);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with seats:{seats} and availability:{availableStatus} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-seats-and-availableDate")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetCarsBySeatsAndAvailableDates(int seats, DateOnly availableDate)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsBySeatsAndAvailableDates(seats, availableDate);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with seats:{seats} and available date:{availableDate} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        //*****************************************************************************************************************************************
        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-availability-and-availableDate")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>>GetCarsByAvailabilityAndAvailableDates(bool availableStatus, DateOnly availableDate)

        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByAvailabilityAndAvailableDates(availableStatus, availableDate);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with availability:{availableStatus} and available date:{availableDate} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        //*****************************************************************************************************************************************
        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-all-filters")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetCarsByAllCarFilters(string model, string make, string colour, decimal price, int seats, bool availableStatus, DateOnly availableDate)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterService.GetCarsByAllCarFilters(model,make,colour,price,seats, availableStatus,availableDate);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }
    }
}
