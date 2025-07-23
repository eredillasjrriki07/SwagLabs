
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwagLabs.Services.AuthServices.Api;
using SwagLabs.Services.AuthServices.Dal;
using SwagLabs.Services.AuthServices.Interfaces;

namespace SwagLabs.Services.AuthServices;

public static class AuthServiceConfiguration
{
    public static IServiceCollection AddAuthServices(this IServiceCollection services, ConfigurationManager config)
    {
        if (config.GetValue<bool>("UseApi"))
        {
            services.AddScoped<IAuthService, AuthApiService>();
        }
        else
        {
            services.AddScoped<IAuthService, AuthDalService>();
        }

        return services;
    }
}
