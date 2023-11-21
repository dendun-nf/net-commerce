using System.Net;
using Microsoft.AspNetCore.Mvc;

namespace Net_Ecommerce.Middleware;

public class CustomExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionHandlerMiddleware> _logger;
    public CustomExceptionHandlerMiddleware(RequestDelegate next, 
        ILogger<CustomExceptionHandlerMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var problemDetails = new ProblemDetails();
        int errorCode = default;
        string? errorTitle = default;
        try
        {
            await _next(context);
        }
        catch (Exception e)
        {
            // the exception is catched so it's not leaking to the response
            // logging the error to the prompt
            _logger.LogError(e, "An error occurred: {ErrorMessage}", e.Message);
            switch (e)
            {
                // TODO: add more cases
                // ex: case NotFoundException:
                //     errorCode = (int)HttpStatusCode.NotFound;
                //     errorTitle = HttpStatusCode.NotFound.ToString();
                //     break;
                default:
                    errorCode = (int)HttpStatusCode.InternalServerError;
                    errorTitle = HttpStatusCode.InternalServerError.ToString();
                    break;
            }

            problemDetails.Instance = context.Request.Path;
            problemDetails.Status = errorCode;
            problemDetails.Title = errorTitle ?? e.Message;
            problemDetails.Type = e.GetType().Name;
            problemDetails.Detail = e.Message;
            problemDetails.Extensions.Add("traceId", context.TraceIdentifier);
            
            context.Response.ContentType = "application/problem+json";
            context.Response.StatusCode = errorCode;
            await context.Response.WriteAsJsonAsync(problemDetails);
        }

    }
}