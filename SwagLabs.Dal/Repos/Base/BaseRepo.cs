
using Microsoft.EntityFrameworkCore;
using SwagLabs.Dal.EfStructures;
using SwagLabs.Models.Entities.Base;

namespace SwagLabs.Dal.Repos.Base;

public abstract class BaseRepo<T> : BaseViewRepo<T>, IBaseRepo<T> where T : BaseEntity, new()
{
    protected BaseRepo(AppDbContext context)
        : base(context)
    {
    }

    protected BaseRepo(DbContextOptions<AppDbContext> options)
        : this(new AppDbContext(options))
    {
    }

    public async Task<int> AddAsync(T entity, bool persist = true)
    {
        await Table.AddAsync(entity);
        return persist ? await SaveChangesAsync() : 0; 
    }

    public async Task<int> AddRangeAsync(IEnumerable<T> entities, bool persist = true)
    {
        await Table.AddRangeAsync(entities);
        return persist ? await SaveChangesAsync() : 0;
    }

    public async Task<int> DeleteAsync(int id, byte[] timeStamp, bool persist = true)
    {
        var entity = new T { Id = id, TimeStamp = timeStamp };
        Context.Entry(entity).State = EntityState.Deleted;
        return persist ? await SaveChangesAsync() : 0;
    }

    public async Task<int> DeleteAsync(T entity, bool persit = true)
    {
        Table.Remove(entity);
        return persit ? await SaveChangesAsync() : 0;
    }

    public async Task<int> DeleteRangeAsync(IEnumerable<T> entities, bool persist = true)
    {
        Table.RemoveRange(entities);
        return persist ? await SaveChangesAsync(): 0;
    }

    public async Task ExecuteParameterizedQueryAsync(string sql, object[] sqlParametersObjects)
        => await Context.Database.ExecuteSqlRawAsync(sql, sqlParametersObjects);

    public async Task<T> FindAsNoTrackingAsync(int id)
        => await Table
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<T> FindAsync(int? id)
        => await Table.FindAsync(id);

    public async Task<T> FindIgnoreQueryFiltersAsync(int id)
        => await Table
            .IgnoreQueryFilters()
            .FirstOrDefaultAsync(x => x.Id == id);

    public async Task<int> SaveChangesAsync()
    {
        try
        {
            return await Context.SaveChangesAsync();
        }
        catch (Exception)
        {
            throw;
        }

    }

    public async Task<int> UpdateAsync(T entity, bool persist = true)
    {
        Table.Update(entity);
        return persist ? await SaveChangesAsync() : 0;
    }

    public async Task<int> UpdateRangeAsync(IEnumerable<T> entities, bool persist = true)
    {
        Table.UpdateRange(entities);
        return persist ? await SaveChangesAsync() : 0;
    }
}
