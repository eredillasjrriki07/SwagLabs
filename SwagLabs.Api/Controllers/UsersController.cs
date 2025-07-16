using SwagLabs.Api.Controllers.Base;
using SwagLabs.Dal.Repos.Interfaces;
using SwagLabs.Models.Entities;
using SwagLabs.Services.Logging.Interfaces;

namespace SwagLabs.Api.Controllers;

public class UsersController : BaseCrudController<User, UsersController>
{
    public UsersController(IAppLogging<UsersController> logger, IUserRepo repo)
        : base(logger, repo)
    {
    }
}
