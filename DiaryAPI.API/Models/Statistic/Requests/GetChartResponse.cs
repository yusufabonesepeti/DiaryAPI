namespace DiaryAPI.API.Models.Statistic.Requests;

public class GetChartResponse
{
    public string Title { get; set; }
    public string Unicode { get; set; }
    public string HexColorCode { get; set; }
    public int Count { get; set; }
}