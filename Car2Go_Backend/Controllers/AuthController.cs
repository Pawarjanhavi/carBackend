using Car2Go.Data;
using Car2Go.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Car2Go.Service;
using Car2Go.DTOs;
using Car2Go.Services;
using log4net;



namespace Car2Go.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration; // Configuration object to access application settings.
        private readonly TokenService _tokenService; // Service for managing refresh tokens.
        private readonly Car2GoDBContext _dbcontext;
        private readonly ILoginService _loginService; // Injecting the login service
        private readonly IUserService _userService;
        private readonly ILog _logger;


        public AuthController(IConfiguration configuration, TokenService tokenService, Car2GoDBContext dbcontext, ILoginService loginService, IUserService userService, ILog logger)
        {
            _configuration = configuration;
            _tokenService = tokenService;
            _dbcontext = dbcontext;
            _loginService = loginService; // Inject the login service
            _userService = userService;
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        // Now, the user will pass parameters directly in the query string or as form data
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromForm] string email, [FromForm] string password)
        {
            // Validate the email and password (Check if they are not null or empty)
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password))
            {
                return BadRequest("Email and password must be provided.");
            }

            try
            {
                // Check if the user credentials are valid
                bool isValidUser = _loginService.LoginUser(email, password);

                if (!isValidUser)
                {
                    return Unauthorized("Invalid user credentials.");
                }

                // If the user is valid, get the user from the database
                var user = await _dbcontext.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    return Unauthorized("Invalid user credentials.");
                }
                else
                {
                    Console.WriteLine($"User found: {user.Email}");  // Log if the user is found
                }


                // Issue a new access token for the user
                var accessToken = IssueAccessToken(user);

                // Generate a new refresh token
                var refreshToken = GenerateRefreshToken();

                // Save the refresh token in the token service (database)
                await _tokenService.SaveRefreshToken(user.Email, refreshToken);

                // Return the generated access and refresh tokens
                return Ok(new { token = accessToken, RefreshToken = refreshToken,role=user.RoleType, Message = "Login successful. Tokens are generated and stored securely." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private string IssueAccessToken(User user)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim("Myapp_User_Id", user.UserId.ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(JwtRegisteredClaimNames.Sub, user.UserId.ToString())
            };

            user.RoleType.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role)));

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }

        [Route("register")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<UserDto> RegisterUser([FromBody] UserDto userDto)
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
            return Ok(result);
        }
    }
}