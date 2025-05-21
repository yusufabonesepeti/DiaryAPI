using System.Net;
using System.Text.Json;
using DiaryAPI.Business.Models.Common;

namespace DiaryAPI.API.Middlewares;

public class GlobalExceptionHandler
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandler> _logger;

    public GlobalExceptionHandler(
        RequestDelegate next,
        ILogger<GlobalExceptionHandler> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    
    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        HttpStatusCode statusCode;
        string message;

        if (exception is BaseException applicationException)
        {
            statusCode = applicationException.Status;
            message = applicationException.Message;
        }
        else
        {
            statusCode = HttpStatusCode.InternalServerError;
            message = "An unexpected error occurred.";
        }
        
        var errorModel = new ErrorModel((int)statusCode, message);
        
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = errorModel.StatusCode;
        await context.Response.WriteAsync(JsonSerializer.Serialize(errorModel));
    }
}