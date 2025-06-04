namespace DiaryAPI.Entities.Entities;

public class MoodEntity : BaseEntity
{
    public string Title { get; set; }
    public string Unicode { get; set; }
    public int Value { get; set; }
    public string HexColor { get; set; }
    public ICollection<EntryEntity> Entries { get; set; }

    public MoodEntity(
        string title,
        string unicode,
        string hexColor,
        int value)
    {
        Title = title;
        Unicode = unicode;
        HexColor = hexColor;
        Value = value;
    }
}