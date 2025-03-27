using System.ComponentModel.DataAnnotations;

namespace Car2Go.Models
{
    public class RefreshTokenRequest
    {
        [Required]
        public string RefreshToken { get; set; }
    }
}