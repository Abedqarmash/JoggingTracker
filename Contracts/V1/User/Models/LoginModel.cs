using System.ComponentModel.DataAnnotations;

namespace Contracts.V1.User.Models
{
    public class LoginModel
    {
        [Required]
        public string Email { get; set; } = default!;

        [Required]
        public string Password { get; set; } =default!;
    }
}
