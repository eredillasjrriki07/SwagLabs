using SwagLabs.Dal.Repos.Base;
using SwagLabs.Dal.Repos.Interfaces;
using SwagLabs.Models.Entities;
using SwagLabs.Services.DataServices.Dal.Base;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;

namespace SwagLabs.Services.DataServices.Dal;

public class ProductDalDataService : DalDataServiceBase<Product, ProductDalDataService>, IProductDataService
{
    public ProductDalDataService(IAppLogging<ProductDalDataService> appLogging, IProductRepo repo) 
        : base(appLogging, repo)
    {
    }

}
