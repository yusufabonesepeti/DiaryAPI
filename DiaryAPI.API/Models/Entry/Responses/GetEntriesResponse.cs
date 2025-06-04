namespace DiaryAPI.API.Models.Entry.Responses;

public class GetEntriesResponse
{
    public Guid Id { get; set; }
    public Guid MoodId { get; set; }
    public string MoodUnicode { get; set; }
    public DateTime CreatedAt { get; set; }
    public string Content { get; set; }
}