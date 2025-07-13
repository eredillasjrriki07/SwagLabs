
using SwagLabs.Dal.Repos.Interfaces;
using SwagLabs.Models.Entities;
using SwagLabs.Services.DataServices.Dal.Base;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;

namespace SwagLabs.Services.DataServices.Dal;

public class UserDalDataService : DalDataServiceBase<User, UserDalDataService>, IUserDataService
{
    public UserDalDataService(IAppLogging<UserDalDataService> appLogging, IUserRepo repo)
        : base(appLogging, repo)
    {
    }
}
