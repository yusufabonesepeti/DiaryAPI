using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using DiaryAPI.Entities.Entities;
using DiaryAPI.Entities.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DiaryAPI.Business.Services;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string CreateAccessToken(UserEntity user)
    {
        var claims = new List<Claim>
        {
            new Claim("Id", user.Id.ToString())
        };
        
        //Security key's symetric key
        SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecurityKey"]));
        
        //Encrypted identity
        SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha256);
        
        JwtSecurityToken securityToken = new(
            audience: _configuration["JwtOptions:Audience"],
            issuer: _configuration["JwtOptions:Issuer"],
            expires: DateTime.UtcNow.AddSeconds(int.Parse(_configuration["JwtOptions:AccessTokenExpireTimeSecond"])),
            notBefore: DateTime.UtcNow,
            claims: claims,
            signingCredentials: signingCredentials
        );
        
        JwtSecurityTokenHandler tokenHandler = new();
        return tokenHandler.WriteToken(securityToken);
    }
}