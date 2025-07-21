
using Microsoft.Extensions.Options;
using SwagLabs.Models.Entities;
using SwagLabs.Services.ApiWrapper.Base;
using SwagLabs.Services.ApiWrapper.Interfaces;
using SwagLabs.Services.ApiWrapper.Models;

namespace SwagLabs.Services.ApiWrapper;

public class UserApiServiceWrapper : ApiServiceWrapperBase<User>, IUserApiServiceWrapper
{
    public UserApiServiceWrapper(HttpClient client, IOptionsMonitor<ApiServiceSettings> apiSettingsMonitor)
        : base(client, apiSettingsMonitor, apiSettingsMonitor.CurrentValue.UserBaseUri)
    {
    }

    public Task<User> LoginWithUserandPasswordAsync(string username, string password)
    {
        throw new NotImplementedException();
    }
}
