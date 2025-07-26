
using Microsoft.Extensions.Options;
using SwagLabs.Models.Entities;
using SwagLabs.Services.ApiWrapper.Auth.Interfaces;
using SwagLabs.Services.ApiWrapper.Models;
using SwagLabs.Services.ApiWrapper.Services.Base;
using SwagLabs.Services.ApiWrapper.Services.Interfaces;

namespace SwagLabs.Services.ApiWrapper.Services;

public class UserApiServiceWrapper : ApiServiceWrapperBase<User>, IUserApiServiceWrapper
{
    public UserApiServiceWrapper(
        HttpClient client,
        IAccessTokenProvider tokenProvider,
        IOptionsMonitor<ApiServiceSettings> apiSettingsMonitor
    ) : base(client, tokenProvider, apiSettingsMonitor, apiSettingsMonitor.CurrentValue.UserBaseUri)
    {
    }

    public Task<User> FindByUsernameAsync(string username)
    {
        throw new NotImplementedException();
    }
}
