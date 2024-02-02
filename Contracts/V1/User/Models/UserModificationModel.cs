namespace Contracts.V1.User.Models
{
    public class UserModificationModel
    {
        public string Email { get; set; } = default!;
        public string UserName { get; set; } = default!;

        public string UserPassword { get; set; } = default!;
        public string UserConfirmationPassword { get; set; } = default!;
    }
}
