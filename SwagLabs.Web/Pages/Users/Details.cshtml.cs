using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SwagLabs.Models.Entities;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;
using SwagLabs.Web.Pages.Base;

namespace SwagLabs.Web.Pages.Users;

public class DetailsModel : BasePageModel<User, DetailsModel>
{

    public DetailsModel(IAppLogging<DetailsModel> appLogging, IUserDataService userService)
        : base(appLogging, userService, "Details")
    {    
    }

    public async Task OnGetAsync(int? id)
    {
        await GetOneAsync(id);
    }
}
