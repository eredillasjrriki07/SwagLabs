using SwagLabs.Dal.Repos.Interfaces;
using SwagLabs.Models.Entities;
using SwagLabs.Services.ApiWrapper.Auth.Models;
using SwagLabs.Services.AuthServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;

namespace SwagLabs.Services.AuthServices.Dal;

public class AuthDalService : IAuthService
{
    protected readonly IUserRepo UserRepo;
    protected readonly IAppLogging<AuthDalService> AppLoggingInstance;

    public AuthDalService(IAppLogging<AuthDalService> appLogging, IUserRepo userRepo)
    {
        AppLoggingInstance = appLogging;
        UserRepo = userRepo;
    }

    public async Task<User?> LoginAsync(LoginRequest loginRequest)
    {
        User user = await UserRepo.FindByUsernameAsync(loginRequest.UserName);

        if (user == null || user.Password != loginRequest.Password)
        {
            return null;
        }

        return user;
    }

    public string CreateJwtToken(User user)
    {
        throw new NotImplementedException();
    }
}
