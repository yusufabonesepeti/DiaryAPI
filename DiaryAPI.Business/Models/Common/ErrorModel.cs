namespace DiaryAPI.Business.Models.Common;

public class ErrorModel
{
    public int StatusCode { get; private set; }
    public string Message { get; private set; }

    public ErrorModel(
        int statusCode,
        string message)
    {
        StatusCode = statusCode;
        Message = message;
    }
}