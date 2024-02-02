using Microsoft.AspNetCore.Http;

namespace Contracts.GlobalExeptionHandler.Exceptions
{
    public class ForbiddenException : ValidateModelException
    {
        public ForbiddenException(string key, string error) : base() => Add(key, error);

        public override int GetStatusCode() => StatusCodes.Status403Forbidden;
    }
}
