using Microsoft.AspNetCore.Http;

namespace Contracts.GlobalExeptionHandler.Exceptions
{
    public class ItemNotFoundException : ValidateModelException
    {
        public ItemNotFoundException(string key, string message) : base() => Add(key, message);

        public override int GetStatusCode() => StatusCodes.Status404NotFound;
    }
}
