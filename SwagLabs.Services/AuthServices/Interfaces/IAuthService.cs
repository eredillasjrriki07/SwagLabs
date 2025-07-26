using Microsoft.Extensions.Options;
using SwagLabs.Models.Entities;
using SwagLabs.Services.ApiWrapper.Auth.Models;

namespace SwagLabs.Services.AuthServices.Interfaces;

public interface IAuthService
{
    Task<User> LoginAsync(LoginRequest loginRequest);

    string CreateJwtToken(User user);
}
