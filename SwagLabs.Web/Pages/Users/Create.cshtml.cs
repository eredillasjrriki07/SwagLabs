using Microsoft.AspNetCore.Mvc;
using SwagLabs.Models.Entities;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;
using SwagLabs.Web.Pages.Base;

namespace SwagLabs.Web.Pages.Users;

public class CreateModel : BasePageModel<User, CreateModel>
{
    public CreateModel(IAppLogging<CreateModel> appLogging, IUserDataService userService)
        : base(appLogging, userService, "Create")
    {
    }

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostCreateNewUserAsync()
    {
        return await SaveOneAsync(DataService.AddAsync);
    }
}
