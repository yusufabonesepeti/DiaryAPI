using System.Net;
using DiaryAPI.Business.Models.Common;

namespace DiaryAPI.Business.Models.Exceptions;

public class NotFoundException : BaseException
{
    public NotFoundException(
        string? message,
        object? details = null) : base(HttpStatusCode.NotFound, message, details)
    {
    }
}