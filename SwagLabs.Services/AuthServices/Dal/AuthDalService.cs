using SwagLabs.Dal.Repos.Interfaces;
using SwagLabs.Models.Entities;
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

    public async Task<User?> LoginAsync(string username, string password)
    {
        User user = await UserRepo.FindByUsernameAsync(username);

        if (user == null || user.Password != password) 
        {
            return null;
        }

        return user;
    }
}
