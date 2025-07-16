using SwagLabs.Models.Entities;
using SwagLabs.Mvc.Controllers.Base;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;

namespace SwagLabs.Mvc.Controllers;

public class UsersController : BaseCrudController<User, UsersController>
{
    public UsersController(IAppLogging<UsersController> appLogging, IUserDataService mainDataService)
        : base(appLogging, mainDataService)
    {
    }
}
