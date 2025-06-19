using System.ComponentModel.DataAnnotations;

namespace CH_BACKEND.Models
{
    public class LoginRequest
    {
        [Required]
        public string UsernameOrEmail { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;
    }
}
