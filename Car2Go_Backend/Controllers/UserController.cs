using Car2Go.Data;
using Car2Go.DTOs;
using Car2Go.Models;
using Car2Go.Services;
using log4net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace Car2Go.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ILog _logger;
        private readonly IUserService _userService;
        public UserController(IUserService userService, ILog logger)
        {
            _userService = userService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles ="Admin")]
        [Route("get-all-users")]
        [HttpGet]
        public ActionResult<IEnumerable<UserDto>> getAllUser()
        {
            IEnumerable<UserDto> result = _userService.GetUsers();
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

        [Route("register")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<UserDto> RegisterUser([FromForm] UserDto userDto)
        {
            var result = _userService.CreateUser(userDto);
            try
            {
                if (result.Email == null)
                {
                    ModelState.AddModelError("CustomError", "User already Exists!");
                    return BadRequest();
                }
                if (userDto == null)
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            //return CreatedAtRoute("getAllUser",userDto);
            return Ok(result);
        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles = "User")]
        //[Route("update-user")]
        //[HttpPut]

        //public IActionResult UpdateUserDetails([FromBody] UpdateUserDto userDto, string email)
        //{
        //    var result = _userService.UpdateUser(userDto, email);
        //    try
        //    {
        //        if (result == false)
        //        {
        //            _logger.Info("User not found");
        //            return BadRequest();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex.Message);
        //    }
        //    return NoContent();
        //}

        [Route("update-user")]
        [HttpPut]

        public IActionResult UpdateUserDetails([FromBody] UpdateUserDto userDto, string email)
        {
            var result = _userService.UpdateUser(userDto, email);
            try
            {
                if (result == false)
                {
                    _logger.Info("User not found");
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, "Internal server error");
            }
            return Ok(new { message = "Updated Successfully" });
        }
        //*******************************************************************************************************************************************

        //[Authorize(Roles = "Admin")]
        [Route("delete-user-account")]
        [HttpDelete]

        public IActionResult DeleteAccount(string email)
        {
            var result = _userService.DeleteUserAccount(email);
            try
            {
                if (result == false)
                {
                    _logger.Info("User not found");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok(new{message = "Delete account Successfully" });
        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles = "Admin")]
        [Route("delete-user")]
        [HttpDelete]

        public IActionResult DeleteUserDetails(string email)
        {
            var result = _userService.DeleteUser(email);
            try
            {
                if (result == false)
                {
                    _logger.Info("User not found");
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                _logger.Error(ex.Message);

            }
            return Ok("User Deleted Successfully");
        }

        //*******************************************************************************************************************************************

        //[Authorize(Roles = "Admin,User")]
        [Route("get-user")]
        [HttpGet]
        public ActionResult<UserDto> GetUser(string email)
        {
            UserDto result = _userService.GetUserByEmail(email);
            try
            {
                if (result.Email == null)
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

        //*************************************************************************

        [Route("reset-password")]
        [HttpPut]

        public IActionResult ResetPassword([FromBody] resetPasswordDto resetPasswordDto, string email)
        {
            var result = _userService.resetUserPassword(resetPasswordDto, email);
            try
            {
                if (result == false)
                {
                    _logger.Info("User not found or password not match");
                    return BadRequest(new {message = "not reset successfully", result=false});
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex.Message);
                return StatusCode(500, "Internal server error");
            }
            return Ok(new { message = "Password Reset Successfully",result = true });
        }

        [Route("get-users-by-role")]
        [HttpGet]
        public ActionResult<List<UserDto>> GetUsersbyRoleUser()
        {
            try
            {
                var users = _userService.GetUsersbyRoleUser();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error fetching users", error = ex.Message });
            }
        }

        [Route("get-agents-by-role")]
        [HttpGet]
        public ActionResult<List<UserDto>> GetUsersByRoleAgent()
        {
            try
            {
                var agents = _userService.GetUsersByRoleAgent(); // Call service method
                return Ok(agents);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Error fetching agents", error = ex.Message });
            }
        }
    }
   

}
