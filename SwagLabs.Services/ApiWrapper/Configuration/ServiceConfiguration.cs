using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwagLabs.Services.ApiWrapper.Interfaces;
using SwagLabs.Services.ApiWrapper.Models;

namespace SwagLabs.Services.ApiWrapper.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection ConfigureApiServiceWrapper(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ApiServiceSettings>(config.GetSection(nameof(ApiServiceSettings)));
        services.AddHttpClient<IUserApiServiceWrapper, UserApiServiceWrapper>();
        return services;
    }
}
