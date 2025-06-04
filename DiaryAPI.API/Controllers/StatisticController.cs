using DiaryAPI.API.Models.Statistic.Requests;
using DiaryAPI.Business.Models.Exceptions;
using DiaryAPI.Entities.Entities;
using DiaryAPI.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiaryAPI.API.Controllers;

[ApiController]
[Route("statistic")]
public class StatisticController : ControllerBase
{
    private readonly IEntryRepository _entryRepository;

    public StatisticController(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    [HttpGet("chart")]
    public async Task<List<GetChartResponse>> GetChart([FromQuery]GetChartRequest request)
    {
        List<EntryEntity?> entries;
        DateTime startDate;
        DateTime endDate;
        
        if (request.Type == ChartType.Weekly)
        {
            (startDate, endDate) = GetWeeklyRange();
        }
        else if (request.Type == ChartType.Monthly)
        {
            (startDate, endDate) = GetMonthlyRange();
        }
        else if (request.Type == ChartType.Yearly)
        {
            (startDate, endDate) = GetYearlyRange();
        }
        else
        {
            throw new BadRequestException("Invalid chart type");
        }
        
        entries = await _entryRepository
            .GetWhere(x => x.CreatedAt >= startDate && x.CreatedAt <= endDate)
            .Include(x => x.Mood)
            .ToListAsync();
        
        var result = entries
            .GroupBy(e => e.Mood)
            .Select(g => new GetChartResponse
            {
                Title = g.Key.Title,
                Unicode = g.Key.Unicode,
                Count = g.Count()
            })
            .ToList();

        return result;
    }

    private (DateTime start, DateTime end) GetWeeklyRange()
    {
        var now = DateTime.UtcNow;
        var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var startOfWeek = now.AddDays(-6);

        if (startOfWeek < startOfMonth)
        {
            startOfWeek = startOfMonth;
        }

        return (startOfWeek, now);
    }

    private (DateTime start, DateTime end) GetMonthlyRange()
    {
        var now = DateTime.UtcNow;
        var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1).AddHours(23).AddMinutes(59).AddSeconds(59);
        
        return (startOfMonth, endOfMonth);
    }
    
    private (DateTime start, DateTime end) GetYearlyRange()
    {
        var now = DateTime.UtcNow;
        var startOfYear = new DateTime(now.Year, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        var endOfYear = new DateTime(now.Year, 12, 31, 23, 59, 59, DateTimeKind.Utc);
        
        return (startOfYear, endOfYear);
    }
}