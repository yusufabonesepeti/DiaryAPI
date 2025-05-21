using System.Net;
using DiaryAPI.Business.Models.Common;

namespace DiaryAPI.Business.Models.Exceptions;

public class BadRequestException : BaseException
{
    public BadRequestException(
        string? message,
        object? details = null) : base(HttpStatusCode.BadRequest, message, details)
    {
    }
}