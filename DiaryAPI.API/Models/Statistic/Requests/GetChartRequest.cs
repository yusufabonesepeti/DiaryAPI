namespace DiaryAPI.API.Models.Statistic.Requests;

public class GetChartRequest
{
    public ChartType Type { get; set; }
}

public enum ChartType
{
    Weekly,
    Monthly,
    Yearly
}