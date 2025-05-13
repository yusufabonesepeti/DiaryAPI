using DiaryAPI.API.Extensions;
using DiaryAPI.API.Models.Auth.Requests;
using DiaryAPI.API.Models.Auth.Responses;
using DiaryAPI.Entities.Entities;
using DiaryAPI.Entities.Repositories;
using DiaryAPI.Entities.Services;
using Microsoft.AspNetCore.Mvc;

namespace DiaryAPI.API.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : Controller
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly ITokenService _tokenService;

    public AuthController(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        ITokenService tokenService)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var user = await _userRepository.GetSingleAsync(user => user.UserName == request.UserName);
        if (user != null)
        {
            return BadRequest("User already exists");
        }

        user = new UserEntity
        {
            UserName = request.UserName,
            Password = _passwordHasher.Hash(request.Password)
        };

        await _userRepository.AddAsync(user);
        await _userRepository.SaveAsync();

        var registerResponse = new RegisterResponse
        {
            UserId = user.Id.ToString(),
            RecoveryKey = user.RecoveryKey
        };

        return Ok(registerResponse);
    }

    [HttpPost("verify-recovery-key")]
    public async Task<IActionResult> VerifyRecoveryKey(VerifyRecoveryKeyRequest request)
    {
        var user = await _userRepository.GetByIdAsync(request.UserId);
        if (user == null)
        {
            return NotFound("User not found");
        }

        if (user.RecoveryKey != request.RecoveryKey)
        {
            return BadRequest("Recovery key is invalid");
        }

        user.Status = "Complete";
        await _userRepository.SaveAsync();

        var token = _tokenService.CreateAccessToken(user);
        
        var tokenResponse = new TokenResponse
        {
            AccessToken = token
        };

        return Ok(tokenResponse);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var user = await _userRepository.GetSingleAsync(user =>
            user.UserName == request.UserName &&
            user.Status == "Complete");
        if (user == null)
        {
            return NotFound("User not found");
        }

        if (_passwordHasher.Verify(user.Password, request.Password))
        {
            var token = _tokenService.CreateAccessToken(user);
            TokenResponse tokenResponse = new TokenResponse
            {
                AccessToken = token
            };
            
            return Ok(tokenResponse);
        }
        
        return BadRequest("Invalid password");
    }
}