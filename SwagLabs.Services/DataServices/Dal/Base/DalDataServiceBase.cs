
using SwagLabs.Dal.Repos.Base;
using SwagLabs.Models.Entities.Base;
using SwagLabs.Services.DataServices.Interfaces;
using SwagLabs.Services.Logging.Interfaces;

namespace SwagLabs.Services.DataServices.Dal.Base;

public class DalDataServiceBase<TEntity, TDataService> : IDataServiceBase<TEntity> 
    where TEntity : BaseEntity, new()
    where TDataService: IDataServiceBase<TEntity>
{
    protected readonly IBaseRepo<TEntity> MainRepo;
    protected readonly IAppLogging<TDataService> AppLoggingInstance;

    public DalDataServiceBase(IAppLogging<TDataService> appLogging, IBaseRepo<TEntity> mainRepo)
    {
        MainRepo = mainRepo;
        AppLoggingInstance = appLogging;
    }

    public async Task<TEntity> AddAsync(TEntity entity, bool persist = true)
    {
        await MainRepo.AddAsync(entity, persist);
        return entity;
    }

    public async Task DeleteAsync(TEntity entity, bool persist = true)
        => await MainRepo.DeleteAsync(entity, persist);

    public async Task<TEntity> FindAsync(int id)
        => await MainRepo.FindAsync(id);

    public async Task<IEnumerable<TEntity>> GetAllAsync()
        => await MainRepo.GetAllIgnoreQueryFiltersAsync();

    public async Task<TEntity> UpdateAsync(TEntity entity, bool persist = true)
    {
        await MainRepo.UpdateAsync(entity, persist);
        return entity;
    }

    public void ResetChangeTracker()
    {
        MainRepo.Context.ChangeTracker.Clear();
    }
}
