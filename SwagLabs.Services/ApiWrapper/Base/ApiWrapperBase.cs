
using System.Net.Http.Headers;
using System.Text;
using Microsoft.Extensions.Options;
using SwagLabs.Services.ApiWrapper.Auth.Interfaces;
using SwagLabs.Services.ApiWrapper.Models;

namespace SwagLabs.Services.ApiWrapper.Base;

public abstract class ApiWrapperBase<TEntity>
{
    protected readonly HttpClient Client;
    protected readonly IAccessTokenProvider TokenProvider;
    protected readonly string _endPoint;
    protected readonly ApiServiceSettings ApiSettings;
    protected readonly string ApiVersion;

    public ApiWrapperBase(
        HttpClient client,
        IAccessTokenProvider tokenProvider,
        IOptionsMonitor<ApiServiceSettings> apiSettingsMonitor,
        string endPoint
    )
    {
        Client = client;
        TokenProvider = tokenProvider;
        _endPoint = endPoint;
        ApiSettings = apiSettingsMonitor.CurrentValue;
        ApiVersion = ApiSettings.ApiVersion;

        Client.BaseAddress = new Uri(ApiSettings.Uri);
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var authToken = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{apiSettingsMonitor.CurrentValue.UserName}:{apiSettingsMonitor.CurrentValue.Password}"));
        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("basic", authToken);
    }

    protected async Task<HttpResponseMessage> PostAsJsonAsync(string uri, string json)
    {
        return await Client.PostAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
    }
}
