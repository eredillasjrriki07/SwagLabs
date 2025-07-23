using SwagLabs.Models.Entities;
using SwagLabs.Services.ApiWrapper.Auth.Models;

namespace SwagLabs.Services.ApiWrapper.Auth.Interfaces;

public interface IAuthApiServiceWrapper
{
    Task<User?> LoginAsync(LoginRequest loginRequest);
}
