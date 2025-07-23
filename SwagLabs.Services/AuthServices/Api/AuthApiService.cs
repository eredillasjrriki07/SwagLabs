using SwagLabs.Dal.Repos.Interfaces;
using SwagLabs.Models.Entities;
using SwagLabs.Services.ApiWrapper.Auth.Interfaces;
using SwagLabs.Services.ApiWrapper.Auth.Models;
using SwagLabs.Services.AuthServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;

namespace SwagLabs.Services.AuthServices.Api;

public class AuthApiService : IAuthService
{

    protected readonly IAuthApiServiceWrapper ServiceWrapper;
    protected readonly IAppLogging<AuthApiService> AppLoggingInstance;

    public AuthApiService(IAppLogging<AuthApiService> appLoggingInstance, IAuthApiServiceWrapper serviceWrapper)
    {
        AppLoggingInstance = appLoggingInstance;
        ServiceWrapper = serviceWrapper;
    }

    public async Task<User?> LoginAsync(LoginRequest loginRequest)
    {
        var user = await ServiceWrapper.LoginAsync(loginRequest);

        if (user == null || user.Password != loginRequest.Password)
        {
            return null;
        }

        return user;
    }

}
