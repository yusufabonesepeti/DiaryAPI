namespace DiaryAPI.Entities.Entities;

public class EntryEntity : BaseEntity
{
    public Guid UserId { get; set; }
    public UserEntity User { get; set; }
    public Guid MoodId { get; set; }
    public MoodEntity Mood { get; set; }
    public string Content { get; set; }

    public EntryEntity(
        Guid userId,
        Guid moodId,
        string content)
    {
        UserId = userId;
        MoodId = moodId;
        Content = content;
    }
}