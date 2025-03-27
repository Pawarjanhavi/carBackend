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
    public class CarFilterByLocationController : ControllerBase
    {
        ICarFilterByLocationService _carFilterByLocationService;
        private readonly ILog _logger;

        public CarFilterByLocationController(ICarFilterByLocationService carFilterByLocationService,ILog logger)
        {
            _carFilterByLocationService = carFilterByLocationService;
            _logger = logger;
        }

        //*******************************************************************************************************************************************

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-city")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByCity(string city)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterByLocationService.GetCarByCity(city);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with city:{city} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        //*******************************************************************************************************************************************

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-state")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsBySate(string state)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterByLocationService.GetCarByState(state);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with state:{state} is not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(result);
        }

        //*******************************************************************************************************************************************

        [Authorize(Roles = "Admin,User")]
        [Route("get-cars-by-city-and-state")]
        [HttpGet]
        public ActionResult<IEnumerable<CarWithLocationDto>> GetAllCarsByCityAndState(string city,string state)
        {
            IEnumerable<CarWithLocationDto> result = _carFilterByLocationService.GetCarByCityAndState(city,state);

            try
            {
                if (result.Count() == 0)
                {
                    _logger.Error($"Car with city:{city} and state:{state} is not found");
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
