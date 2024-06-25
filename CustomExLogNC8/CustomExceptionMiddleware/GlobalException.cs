using CustomExLogNC8.Models;
using LoggerService;
using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace CustomExLogNC8.CustomExceptionMiddleware
{
    public class GlobalExceptionHandler: IExceptionHandler
    {
        private readonly ILoggerManager _logger;
        public GlobalExceptionHandler(ILoggerManager logger) => _logger = logger;
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, 
            CancellationToken cancellationToken)
        {
            httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            httpContext.Response.ContentType = "application/json";

            _logger.LogError($"Something went wrong: {exception}");

            var message = exception switch
            {
                AccessViolationException => "Access violation exception from the custom middleware",
                _ => "Internal server error from the custom middleware."
            };

            await httpContext.Response.WriteAsync(new ErrorDetails()
            {
                StatusCode = httpContext.Response.StatusCode,
                Message = message
            }.ToString());

            return true;
        }
    }
}
