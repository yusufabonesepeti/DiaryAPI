namespace DiaryAPI.API.Models.Auth.Requests;

public class LoginRequest
{
    public string UserName { get; set; }
    public string Password { get; set; }
}