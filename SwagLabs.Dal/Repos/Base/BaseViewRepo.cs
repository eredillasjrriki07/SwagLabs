
using Microsoft.EntityFrameworkCore;
using SwagLabs.Dal.EfStructures;

namespace SwagLabs.Dal.Repos.Base;

public abstract class BaseViewRepo<T> : IBaseViewRepo<T> where T : class, new()
{
    private readonly bool _disposeContext;

    private bool _isDisposed;

    public DbSet<T> Table { get; }

    public AppDbContext Context { get; }

    protected BaseViewRepo(AppDbContext context)
    {
        Context = context;
        Table = Context.Set<T>();
        _disposeContext = false;
    }

    protected BaseViewRepo(DbContextOptions<AppDbContext> options)
        : this(new AppDbContext(options))
    {
        _disposeContext = false;
    }

    public virtual async Task<IEnumerable<T>> ExecuteSqlStringAsync(string sql)
        => await Table.FromSqlRaw(sql).ToListAsync();

    public virtual async Task<IEnumerable<T>> GetAllAsync()
        => await Table.ToListAsync();

    public virtual async Task<IEnumerable<T>> GetAllIgnoreQueryFiltersAsync()
        => await Table.IgnoreQueryFilters().ToListAsync();


    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }

        if (disposing)
        {
            if (_disposeContext)
            {
                Context.Dispose();
            }
        }

        _isDisposed = true;
    }

    ~BaseViewRepo()
    {
        Dispose(false);
    }
}
