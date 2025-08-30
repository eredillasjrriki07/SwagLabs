
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using SwagLabs.Dal.Repos;
using SwagLabs.Dal.Repos.Interfaces;
using SwagLabs.Services.DataServices.Api;
using SwagLabs.Services.DataServices.Dal;
using SwagLabs.Services.DataServices.Interfaces;

namespace SwagLabs.Services.DataServices;

public static class DataServiceConfiguration
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IUserRepo, UserRepo>();
        services.AddScoped<IProductRepo, ProductRepo>();
        return services;
    }

    public static IServiceCollection AddDataServices(this IServiceCollection services, ConfigurationManager config)
    {
        if (config.GetValue<bool>("UseApi"))
        {
            services.AddScoped<IUserDataService, UserApiDataService>();
        }
        else 
        {
            services.AddScoped<IUserDataService, UserDalDataService>();
            services.AddScoped<IProductDataService, ProductDalDataService>();
        }
        return services;
    }
}
