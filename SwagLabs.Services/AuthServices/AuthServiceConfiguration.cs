
using Azure.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SwagLabs.Services.ApiWrapper.Auth;
using SwagLabs.Services.ApiWrapper.Auth.Interfaces;
using SwagLabs.Services.ApiWrapper.Auth.Models;
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
            services.Configure<JwtSettings>(config.GetSection(nameof(JwtSettings)));
            services.AddHttpContextAccessor();
            services.AddScoped<IAccessTokenProvider, AccessTokenProvider>();
            services.AddScoped<IAuthService, AuthApiService>();
        }
        else
        {
            services.AddScoped<IAuthService, AuthDalService>();
        }

        return services;
    }
}
