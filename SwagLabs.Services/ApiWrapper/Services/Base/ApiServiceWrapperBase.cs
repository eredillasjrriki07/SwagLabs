using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using SwagLabs.Models.Entities.Base;
using SwagLabs.Services.ApiWrapper.Base;
using SwagLabs.Services.ApiWrapper.Models;
using SwagLabs.Services.ApiWrapper.Services.Interfaces;

namespace SwagLabs.Services.ApiWrapper.Services.Base;

public class ApiServiceWrapperBase<TEntity> : ApiWrapperBase<TEntity>, IApiServiceWrapperBase<TEntity>
    where TEntity : BaseEntity, new()
{
    public ApiServiceWrapperBase(HttpClient client, IOptionsMonitor<ApiServiceSettings> apiSettingsMonitor, string endPoint)
        : base(client, apiSettingsMonitor, endPoint)
    {
    }

    public async Task<TEntity> AddEntityAsync(TEntity entity)
    {
        var response = await PostAsJsonAsync($"{ApiSettings.Uri}{_endPoint}?v={ApiVersion}", JsonSerializer.Serialize(entity));

        if (response == null)
        {
            throw new Exception("Unable to communicate with the service");
        }

        var location = response.Headers?.Location?.OriginalString;

        return await response.Content.ReadFromJsonAsync<TEntity>() ?? await GetEntityAsync(entity.Id);
    }

    public async Task DeleteEntityAsync(TEntity entity)
    {
        var response = await DeleteAsJsonAsync($"{ApiSettings.Uri}{_endPoint}/{entity.Id}?v={ApiVersion}", JsonSerializer.Serialize(entity));
        response.EnsureSuccessStatusCode();
    }

    public async Task<IList<TEntity>> GetAllEntitiesAsync()
    {
        string url = $"{ApiSettings.Uri}{_endPoint}?v={ApiVersion}";
        var response = await Client.GetAsync(url);
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<IList<TEntity>>();
        return result;
    }

    public async Task<TEntity> GetEntityAsync(int id)
    {
        var response = await Client.GetAsync($"{ApiSettings.Uri}{_endPoint}/{id}?v={ApiVersion}");
        response.EnsureSuccessStatusCode();
        var result = await response.Content.ReadFromJsonAsync<TEntity>();
        return result;
    }

    public async Task<TEntity> UpdateEntityAsync(TEntity entity)
    {
        var response = await PutAsJsonAsync($"{ApiSettings.Uri}{_endPoint}/{entity.Id}?v={ApiVersion}", JsonSerializer.Serialize(entity));
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<TEntity>() ?? await GetEntityAsync(entity.Id);
    }

    internal async Task<HttpResponseMessage> PutAsJsonAsync(string uri, string json)
    {
        return await Client.PutAsync(uri, new StringContent(json, Encoding.UTF8, "application/json"));
    }

    internal async Task<HttpResponseMessage> DeleteAsJsonAsync(string uri, string json)
    {
        HttpRequestMessage request = new HttpRequestMessage
        {
            Content = new StringContent(json, Encoding.UTF8, "application/json"),
            Method = HttpMethod.Delete,
            RequestUri = new Uri(uri)
        };

        return await Client.SendAsync(request);

    }
}
