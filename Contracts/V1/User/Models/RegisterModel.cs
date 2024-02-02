using System.ComponentModel.DataAnnotations;
using Contracts.Constants;

namespace Contracts.V1.User.Models
{
    public class RegisterModel
    {
        /// <summary>
        /// User name
        /// </summary>
        [Required]
        public string UserName { get; set; } = default!;

        /// <summary>
        /// User Email
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = default!;

        /// <summary>
        /// User Password
        /// </summary>
        [Required]
        [StringLength(Constant.PasswordLength, ErrorMessage = ValidationMessages.PasswordsLengthMessage, MinimumLength = 5)]
        [DataType(DataType.Password)]
        public string Password { get; set; } = default!;

        [Required]
        [Compare("Password", ErrorMessage = ValidationMessages.ConfirmationPasswordsMessage)]
        public string ConfirmPassword { get; set; } = default!;
    }
}
