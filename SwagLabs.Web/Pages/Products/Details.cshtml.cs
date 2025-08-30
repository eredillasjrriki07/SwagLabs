using SwagLabs.Models.Entities;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;
using SwagLabs.Web.Pages.Base;

namespace SwagLabs.Web.Pages.Products;

public class DetailsModel : BasePageModel<Product, DetailsModel>
{
    public DetailsModel(IAppLogging<DetailsModel> appLogging, IProductDataService dataService)
        : base(appLogging, dataService, "Product")
    {
    }

    public async Task OnGet(int? id)
    {
        await GetOneAsync(id);
    }

}
