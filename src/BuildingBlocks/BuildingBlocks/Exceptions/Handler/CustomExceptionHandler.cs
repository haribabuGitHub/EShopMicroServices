//using System.Web.Http.ExceptionHandling;

using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.ComponentModel.DataAnnotations;

namespace BuildingBlocks.Exceptions.Handler
{
    public class CustomExceptionHandler(ILogger<CustomExceptionHandler> logger) : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(HttpContext context, Exception exception, CancellationToken cancellationToken)
        {
            logger.LogError("Error Message :{exceptionMessage} Time of Occurence : {time}", exception.Message, DateTime.UtcNow);

            (string Details,string Title, int StatusCode) details = exception switch
            {
                BadRequestException badRequestException => 
                (badRequestException.Details, 
                badRequestException.Message,
                StatusCodes.Status400BadRequest),

                InternalServerException internalServerException => 
                (internalServerException.Details, 
                internalServerException.Message, 
                StatusCodes.Status500InternalServerError),

                NotFoundException notFoundException => (notFoundException.Message,
                notFoundException.Message, 
                StatusCodes.Status404NotFound),

                ValidationException validationEx => (
                validationEx.Message,
                validationEx.GetType().Name,
                StatusCodes.Status404NotFound
                ),

                _ => (string.Empty, "An unexpected error occurred.", StatusCodes.Status500InternalServerError)
            };

            var problemDetails = new ProblemDetails
            {
                Title = details.Title,
                Detail = details.Details,
                Status = details.StatusCode,
                Instance = context.Request.Path
            };

            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);

            if (exception is ValidationException validationException)
            {                 
                problemDetails.Extensions.Add("validationErrors", validationException.ValidationResult);
            }

            await context.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;

        }
    }
}
