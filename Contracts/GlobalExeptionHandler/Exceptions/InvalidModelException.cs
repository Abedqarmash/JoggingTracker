using Microsoft.AspNetCore.Http;

namespace Contracts.GlobalExeptionHandler.Exceptions
{
    public class InvalidModelException : ValidateModelException
    {
        public InvalidModelException(string key, string error) : base() => Add(key, error);

        public override int GetStatusCode() => StatusCodes.Status400BadRequest;
    }
}
