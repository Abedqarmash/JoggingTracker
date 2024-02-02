namespace Contracts.V1.User.Resources
{
    public class UserResource
    {
        public string UserName { get; set; } = default!;

        public string Email { get; set; } = default!;

        public string? Token { get; set; }

        public DateTimeOffset? ExpireAt { get; set; } = default!;

        public IEnumerable<string> roles { get; set; } = Enumerable.Empty<string>();

    }
}
