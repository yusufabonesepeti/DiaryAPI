using DiaryAPI.API.Extensions;
using DiaryAPI.API.Models.Entry.Requests;
using DiaryAPI.API.Models.Entry.Responses;
using DiaryAPI.Entities.Entities;
using DiaryAPI.Entities.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiaryAPI.API.Controllers;

[ApiController]
[Route("entry")]
public class EntryController : ControllerBase
{
    private readonly IEntryRepository _entryRepository;
    public EntryController(IEntryRepository entryRepository)
    {
        _entryRepository = entryRepository;
    }

    [Authorize]
    [HttpGet]
    public async Task<List<GetEntriesResponse>> GetEntries([FromQuery]GetEntriesRequest request)
    {
        var userId = User.GetUserId();
        var userGuid = Guid.Parse(userId);

        var entries = await _entryRepository.GetWhere(x =>
            x.CreatedAt.Month == request.Date.Month &&
            x.CreatedAt.Year == request.Date.Year &&
            x.UserId == userGuid)
            .Include(x => x.Mood)
            .ToListAsync();

        return entries.Select(x => new GetEntriesResponse
        {
            Id = x.Id,
            MoodId = x.MoodId,
            MoodUnicode = x.Mood.Unicode,
            Content = x.Content,
            CreatedAt = x.CreatedAt
        }).ToList();
    }
    
    [Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateEntry(CreateEntryRequest request)
    {
        var userId = User.GetUserId();
        var userGuid = Guid.Parse(userId);

        var entry = await _entryRepository.GetSingleAsync(x =>
            x.UserId == userGuid &&
            x.CreatedAt.Date == request.Date.Date);

        if (entry == null)
        {
            entry = new EntryEntity(userGuid, request.MoodId, request.Content);
            await _entryRepository.AddAsync(entry);
            await _entryRepository.SaveAsync();

            return Ok();
        }

        entry.MoodId = request.MoodId;
        entry.Content = request.Content;
        await _entryRepository.SaveAsync();

        return Ok();
    }
}