
using SwagLabs.Models.Entities;
using SwagLabs.Services.ApiWrapper.Services.Interfaces;
using SwagLabs.Services.DataServices.Api.Base;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;

namespace SwagLabs.Services.DataServices.Api;

public class UserApiDataService : ApiDataServiceBase<User, UserApiDataService>, IUserDataService
{
    public UserApiDataService(IAppLogging<UserApiDataService> appLogging, IUserApiServiceWrapper serviceWrapper)
        : base(appLogging, serviceWrapper)
    {
    }

}
