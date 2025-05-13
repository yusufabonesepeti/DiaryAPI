namespace DiaryAPI.API.Models.Auth.Requests;

public class VerifyRecoveryKeyRequest
{
    public Guid UserId { get; set; }
    public string RecoveryKey { get; set; }
}