namespace DiaryAPI.API.Models.Entry.Requests;

public class CreateEntryRequest
{
    public Guid MoodId { get; set; }
    public string Content { get; set; }
    public DateTime Date { get; set; }
}