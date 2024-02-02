
using Contracts.V1.SharedModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;

namespace Contracts.GlobalExeptionHandler
{
    public class GlobalExceptionHandler : IExceptionFilter
    {
        private readonly IHostEnvironment _env;

        public GlobalExceptionHandler(IHostEnvironment env)
        {
            _env = env;
        }
        public void OnException(ExceptionContext context)
        {
            var errorReponse = new ErrorResponse();

            switch (context.Exception)
            {
                case ValidateModelException validateModelException:
                    errorReponse.ErrorDetails = validateModelException.GetErrors();
                    errorReponse.StatusCode = validateModelException.GetStatusCode();
                    break;

                default:
                    errorReponse.Message = context.Exception.Message;
                    errorReponse.StatusCode = StatusCodes.Status500InternalServerError;
                    break;
            }

            if (_env.IsDevelopment())
            {
                errorReponse.Message = context.Exception.Message;
                errorReponse.StackTrace = context.Exception.StackTrace;
            }

            context.Result = new ObjectResult(errorReponse)
            {
                StatusCode = errorReponse.StatusCode,
                DeclaredType = typeof(ErrorResponse)
            };
        }
    }
}
