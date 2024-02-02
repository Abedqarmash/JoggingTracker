using Microsoft.AspNetCore.Http;

namespace Contracts.GlobalExeptionHandler.Exceptions
{
    public class ConflictException : ValidateModelException
    {
        public ConflictException(string key, string error) : base() => Add(key, error);

        public override int GetStatusCode() => StatusCodes.Status409Conflict;
    }
}
