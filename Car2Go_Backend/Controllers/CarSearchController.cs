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
    public class CarSearchController : ControllerBase
    {
        private readonly ICarSearchService _carSearchService;
        private readonly ILog _logger;
        public CarSearchController(ICarSearchService carSearchService,ILog logger)
        {
            _carSearchService = carSearchService;
            _logger = logger;
        }

        //[Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-MakeOrModel")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithRatingsDto>> GetAllCarsBySearchValue([FromQuery]string searchValue)
        {
            IEnumerable<CarWithRatingsDto> result = _carSearchService.GetAllCarsBySearchValue(searchValue);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with company:{searchValue} is not found");
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
        //[Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-filters")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithRatingsDto>> GetCarsByAllCarFilters(string Colour, int year, int TotalSeats, bool AvailableStatus)
        {
            IEnumerable<CarWithRatingsDto> result = _carSearchService.GetCarsByCarFilters(Colour, year, TotalSeats, AvailableStatus);

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

        //public ActionResult<IEnumerable<CarWithImageDto>> GetCarsByAllCarFilters(string City, string State, string Colour, int year, int TotalSeats, bool AvailableStatus, decimal minPrice, decimal maxPrice)
        //{
        //    IEnumerable<CarWithImageDto> result = _carSearchService.GetCarsByCarFilters(City, State, Colour, year, TotalSeats, AvailableStatus, minPrice, maxPrice);

        //    try
        //    {
        //        if (result.Count() == 0)
        //        {
        //            _logger.Error($"Car is not found");
        //            return NotFound();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message);

        //    }
        //    return Ok(result);
        //}

        //***********************************************************************

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-make")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByMake(string make)
        {
            IEnumerable<CarWithLocationDto> result = _carSearchService.GetCarByMake(make);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with company:{make} is not found");
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
        [Route("get-cars-by-model")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByModel( string model)
        {
            IEnumerable<CarWithLocationDto> result = _carSearchService.GetCarByModel(model);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with model:{model} is not found");
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
        [Route("get-cars-by-colour")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByColour( string colour)
        {
            IEnumerable<CarWithLocationDto> result = _carSearchService.GetCarByColour(colour);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with colour:{colour} is not found");
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
        [Route("get-cars-by-price")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByPrice( decimal price)
        {
            IEnumerable<CarWithLocationDto> result = _carSearchService.GetCarByPrice(price);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with price:{price} is not found");
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
        [Route("get-cars-by-seat")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsBySeat( int seats)
        {
            IEnumerable<CarWithLocationDto> result = _carSearchService.GetCarBySeat(seats);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with seats:{seats} is not found");
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
        [Route("get-cars-by-availability")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByMake(  bool availability)
        {
            IEnumerable<CarWithLocationDto> result = _carSearchService.GetCarByAvailability(availability);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car is not found with availability status ");
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
        [Route("get-cars-by-available-date")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByAvailableDate(  DateOnly availableDate)
        {
            IEnumerable<CarWithLocationDto> result = _carSearchService.GetCarByAvailableDates(availableDate);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with date:{availableDate} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        //[Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-price-range")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithRatingsDto>> GetAllCarsByPriceRange(decimal maxPrice, decimal minPrice)
        {
            IEnumerable<CarWithRatingsDto> result = _carSearchService.GetCarByPriceRange(maxPrice,minPrice);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with price range between {minPrice} to {maxPrice} is not found");
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
