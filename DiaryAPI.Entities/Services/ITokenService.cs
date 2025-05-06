using DiaryAPI.Entities.Entities;

namespace DiaryAPI.Entities.Services;

public interface ITokenService
{
    public string CreateAccessToken(UserEntity user);
}