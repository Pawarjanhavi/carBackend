using Car2Go.Data;
using Car2Go.DTOs;
using Car2Go.Models;
using Car2Go.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Car2Go.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocationController : ControllerBase
    {

        ILocationService _locationService;
        ILog _logger;
        public LocationController(ILocationService locationService,ILog logger)
        {
            _locationService = locationService;
            _logger = logger;
        }

        //*******************************************************************************************************************************************

        [Authorize(Roles = "Admin,Agent")]
        [Route("GetAll")]
        [HttpGet]
        public ActionResult<IEnumerable<LocationDto>> GetAllLocations()
        {

            IEnumerable<LocationDto> result = _locationService.GetLocations();

            try
            {
                if (result == null)
                {
                    _logger.Error("Location not found");
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

        [Authorize(Roles = "Admin,Agent")]
        [Route("Create")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<LocationDto> AddNewLocation([FromBody] LocationDto locationDto)
        {
            LocationDto result = _locationService.CreateLocation(locationDto);
            try
            {
                if (result == null)
                {
                    ModelState.AddModelError("CustomError", "Location already Exists!");
                    return BadRequest();
                }
                if (locationDto == null)
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return Ok(locationDto);
            //return CreatedAtRoute("GetAllLocations", locationDto);
        }

        //*******************************************************************************************************************************************

        [Authorize(Roles = "Admin,Agent")]
        [Route("Update")]
        [HttpPut]
        public IActionResult UpdateLocationDetails(string address, [FromBody] LocationDto locationDto)
        {
            var result = _locationService.UpdateLocation(address, locationDto);
            try
            {
                if (result == false)
                {
                    _logger.Error("Location not exist");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return Ok(new { result = true });
        }

        //*******************************************************************************************************************************************

        [Authorize(Roles = "Admin")]
        [Route("delete")]
        [HttpDelete]
        public IActionResult DeleteLocationDetails(string address)
        {
            var result = _locationService.DeleteLocation(address);
            try
            {
                if (result == false)
                {
                    _logger.Error("Location not exist");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
            }
            return Ok(new { result = true });
        }
    }
}
