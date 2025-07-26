using SwagLabs.Models.Entities;

namespace SwagLabs.Services.ApiWrapper.Auth.Interfaces;

public interface IAccessTokenProvider
{
    string Create(User user);
    string Get();
}
