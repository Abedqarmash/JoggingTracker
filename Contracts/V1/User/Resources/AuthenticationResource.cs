namespace Contracts.V1.User.Resources
{
    public class AuthenticationResource
    {
        public string? Token { get; set; }
        public DateTimeOffset ExpireAt { get; set; }
    }
}
