
using SwagLabs.Dal.EfStructures;

namespace SwagLabs.Dal.Repos.Base;

public interface IBaseViewRepo<T> : IDisposable where T : class, new()
{
    AppDbContext Context { get; }
    Task<IEnumerable<T>> ExecuteSqlStringAsync(string sql);
    Task<IEnumerable<T>> GetAllAsync();
    Task<IEnumerable<T>> GetAllIgnoreQueryFiltersAsync();
}
