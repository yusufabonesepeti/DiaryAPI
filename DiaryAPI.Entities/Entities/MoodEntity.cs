namespace DiaryAPI.Entities.Entities;

public class MoodEntity : BaseEntity
{
    public string Title { get; set; }
    public string Unicode { get; set; }
    public int Value { get; set; }
    public ICollection<EntryEntity> Entries { get; set; }

    public MoodEntity(string title, string unicode, int value)
    {
        Title = title;
        Unicode = unicode;
        Value = value;
    }
}