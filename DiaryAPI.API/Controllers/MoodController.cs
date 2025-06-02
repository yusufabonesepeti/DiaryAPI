using DiaryAPI.API.Models.Mood.Responses;
using DiaryAPI.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiaryAPI.API.Controllers;

[ApiController]
[Route("mood")]
public class MoodController : ControllerBase
{
    private readonly IMoodRepository _moodRepository;
    public MoodController(IMoodRepository moodRepository)
    {
        _moodRepository = moodRepository;
    }

    [HttpGet]
    public async Task<List<GetMoodsResponse>> GetMoods()
    {
        var moods = await _moodRepository.GetAll().ToListAsync();

        return moods.Select(x => new GetMoodsResponse
        {
            Id = x.Id,
            Title = x.Title,
            Unicode = x.Unicode,
            Value = x.Value
        }).ToList();
    }
}