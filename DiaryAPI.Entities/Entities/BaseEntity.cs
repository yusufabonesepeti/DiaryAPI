using Swashbuckle.AspNetCore.Annotations;

namespace DiaryAPI.Entities.Entities;

public class BaseEntity
{
    [SwaggerIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();
    [SwaggerIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}