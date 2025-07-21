using Microsoft.AspNetCore.Mvc;
using SwagLabs.Models.Entities;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;
using SwagLabs.Web.Pages.Base;

namespace SwagLabs.Web.Pages.Users;

public class EditModel : BasePageModel<User, EditModel>
{
    public EditModel(IAppLogging<EditModel> appLogging, IUserDataService userService)
        : base(appLogging, userService, "Edit")
    {

    }

    public async Task OnGetAsync(int? id)
    {
        await GetOneAsync(id);

    }

    public async Task<IActionResult> OnPostAsync()
    {
        return await SaveOneAsync(DataService.UpdateAsync);
    }
}
