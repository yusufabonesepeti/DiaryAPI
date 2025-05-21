using System.Net;

namespace DiaryAPI.Business.Models.Common;

public class BaseException : Exception
{
    public HttpStatusCode Status { get; private set; } //400
    public string Message { get; private set; } // "Not Found"
    public object? Details { get; private set; }

    public BaseException(
        HttpStatusCode statusCode,
        string? message,
        object? details = null) : base(message)
    {
        Status = statusCode;
        Message = message ?? string.Empty;
        Details = details;
    }
}