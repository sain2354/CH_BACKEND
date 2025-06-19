using System.ComponentModel.DataAnnotations;

namespace CH_BACKEND.Models
{
    public class GoogleLoginRequest
    {
        [Required]
        public string Email { get; set; } = string.Empty;

        public string? DisplayName { get; set; }
        public string? Phone { get; set; }
    }
}
