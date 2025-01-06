using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

public class CustomException
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomException> _logger;

    public CustomException(RequestDelegate next, ILogger<CustomException> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context); // Call the next middleware in the pipeline
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        // Log the exception details
        _logger.LogError(exception, "Unhandled exception occurred");

        // Create a response with error details
        var response = context.Response;
        response.ContentType = "application/json";
        response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorDetails = new
        {
            StatusCode = response.StatusCode,
            Message = "An internal server error occurred. Please try again later.",
            Details = exception.Message
        };

        // Log the error in a structured format
        var errorLog = JsonSerializer.Serialize(new
        {
            Timestamp = DateTime.UtcNow,
            Path = context.Request.Path,
            QueryString = context.Request.QueryString.ToString(),
            Method = context.Request.Method,
            ErrorMessage = exception.Message,
            StackTrace = exception.StackTrace
        });

        _logger.LogError(errorLog);

        // Send JSON response
        var errorJson = JsonSerializer.Serialize(errorDetails);
        await response.WriteAsync(errorJson);
    }
}