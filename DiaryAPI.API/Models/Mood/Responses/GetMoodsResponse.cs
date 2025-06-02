namespace DiaryAPI.API.Models.Mood.Responses;

public class GetMoodsResponse
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Unicode { get; set; }
    public int Value { get; set; }
}