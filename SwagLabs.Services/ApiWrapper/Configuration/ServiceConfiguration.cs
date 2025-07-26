using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwagLabs.Services.ApiWrapper.Auth;
using SwagLabs.Services.ApiWrapper.Auth.Interfaces;
using SwagLabs.Services.ApiWrapper.Auth.Models;
using SwagLabs.Services.ApiWrapper.Models;
using SwagLabs.Services.ApiWrapper.Services;
using SwagLabs.Services.ApiWrapper.Services.Interfaces;

namespace SwagLabs.Services.ApiWrapper.Configuration;

public static class ServiceConfiguration
{
    public static IServiceCollection ConfigureApiServiceWrapper(this IServiceCollection services, IConfiguration config)
    {
        services.Configure<ApiServiceSettings>(config.GetSection(nameof(ApiServiceSettings)));
        services.AddHttpClient<IUserApiServiceWrapper, UserApiServiceWrapper>();
        services.AddHttpClient<IAuthApiServiceWrapper, AuthApiServiceWrapper>();
        return services;
    }
}
