namespace DiaryAPI.API.Models.Auth.Responses;

public class RegisterResponse
{
    public string UserId { get; set; }
    public string RecoveryKey { get; set; }
}