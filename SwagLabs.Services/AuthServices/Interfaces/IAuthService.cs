using SwagLabs.Models.Entities;

namespace SwagLabs.Services.AuthServices.Interfaces;

public interface IAuthService
{
    Task<User?> LoginAsync(string username, string password);
}
