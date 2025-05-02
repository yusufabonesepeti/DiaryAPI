using DiaryAPI.Entities.Entities;
using DiaryAPI.Entities.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DiaryAPI.API.Controllers;

[ApiController]
[Route("Test")]
public class TestController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public TestController(
        IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("create-user")]
    public async Task<IActionResult> CreateUser(UserEntity user)
    {
        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();

        return Ok("Kullanıcı oluşturuldu");
    }

    [HttpGet("get-users")]
    public async Task<IActionResult> GetUsers()
    {
        var users = _userRepository.GetAll();
        return Ok(users);
    }

    [HttpGet("get-user-by-id")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound("Kullanıcı bulunamadı");
        }
        
        return Ok(user);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(string username, string password)
    {
        var users = await _userRepository
            .GetWhere(userEntity => userEntity.UserName == username && userEntity.Password == password)
            .ToListAsync();

        if (users == null || users.Count == 0)
        {
            return NotFound("Böyle bir kullanıcı bulunamadı");
        }

        return Ok("Giriş yapıldı");
    }

    [HttpDelete("delete-user")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound("Kullanıcı bulunamadı");
        }

        _userRepository.Delete(user);
        await _userRepository.SaveAsync();
        
        return Ok("Kullanıcı silindi");
    }
}