using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SwagLabs.Models.Entities;
using SwagLabs.Services.ApiWrapper.Auth.Interfaces;
using SwagLabs.Services.ApiWrapper.Auth.Models;

namespace SwagLabs.Services.ApiWrapper.Auth;

public class AccessTokenProvider : IAccessTokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IOptionsMonitor<JwtSettings> _jwtSettingsMonitor;

    public AccessTokenProvider(IHttpContextAccessor httpContextAccessor, IOptionsMonitor<JwtSettings> jwtSettingsMonitor)
    {
        _httpContextAccessor = httpContextAccessor;
        _jwtSettingsMonitor = jwtSettingsMonitor;
    }

    public string Create(User user)
    {
        JwtSecurityTokenHandler tokenHandler = new();

        byte[] key = Encoding.ASCII.GetBytes(_jwtSettingsMonitor.CurrentValue.Key);

        SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            ]),
            Expires = DateTime.UtcNow.AddMinutes(_jwtSettingsMonitor.CurrentValue.ExpireMinutes),
            Issuer = _jwtSettingsMonitor.CurrentValue.Issuer,
            Audience = _jwtSettingsMonitor.CurrentValue.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);

        return tokenHandler.WriteToken(token);
    }

    public string Get()
    {
        string token = _httpContextAccessor.HttpContext?.Request.Cookies["accessToken"];

        return token;
    }
}
