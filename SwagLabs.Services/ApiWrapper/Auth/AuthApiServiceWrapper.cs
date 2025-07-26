using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Options;
using SwagLabs.Models.Entities;
using SwagLabs.Services.ApiWrapper.Auth.Interfaces;
using SwagLabs.Services.ApiWrapper.Auth.Models;
using SwagLabs.Services.ApiWrapper.Base;
using SwagLabs.Services.ApiWrapper.Models;

namespace SwagLabs.Services.ApiWrapper.Auth;

public class AuthApiServiceWrapper : ApiWrapperBase<User>, IAuthApiServiceWrapper
{

    public AuthApiServiceWrapper(
        HttpClient client,
        IAccessTokenProvider tokenProvider,
        IOptionsMonitor<ApiServiceSettings> apiSettingsMonitor
    ) : base(client, tokenProvider, apiSettingsMonitor, apiSettingsMonitor.CurrentValue.AuthBaseUri)
    {
    }

    public async Task<User> LoginAsync(LoginRequest loginRequest)
    {
        var response = await PostAsJsonAsync($"{ApiSettings.Uri}{_endPoint}?v={ApiVersion}", JsonSerializer.Serialize(loginRequest));

        if (response == null)
        {
            throw new Exception("Unable to communicate with the service");
        }

        var location = response.Headers?.Location?.OriginalString;

        return await response.Content.ReadFromJsonAsync<User>();

    }

    public string CreateJwtToken(User user) => TokenProvider.Create(user);
}
