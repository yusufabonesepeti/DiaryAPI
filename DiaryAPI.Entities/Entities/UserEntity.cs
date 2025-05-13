using Swashbuckle.AspNetCore.Annotations;

namespace DiaryAPI.Entities.Entities;

public class UserEntity : BaseEntity
{
    public string UserName { get; set; }
    public string Password { get; set; }
    [SwaggerIgnore]
    public string RecoveryKey { get; set; } = Guid.NewGuid().ToString();

    public string Status { get; set; } = "Incomplete";
}