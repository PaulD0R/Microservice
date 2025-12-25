using System.Net;
using System.Text.Json;
using HotelService.Domain.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace HotelService.API.Exceptions;

public class ExceptionHandler(
    ILogger<ExceptionHandler> logger)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        logger.LogError(exception, exception.Message);

        var status = exception switch
        {
            NotFoundException => (int)HttpStatusCode.NotFound,
            BadRequestException => (int)HttpStatusCode.BadRequest,
            UnauthorizedAccessException => (int)HttpStatusCode.Unauthorized,
            _ => (int)HttpStatusCode.InternalServerError
        };

        var problem = new ProblemDetails
        {
            Type = $"https://httpstatuses.io/{status}",
            Title =  exception.GetType().Name,
            Detail = exception.Message,
            Status = status,
            Instance = httpContext.Request.Path,
        };

        httpContext.Response.ContentType = "application/problem+json";
        httpContext.Response.StatusCode = status;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(problem), cancellationToken);
        
        return true;
    }
}