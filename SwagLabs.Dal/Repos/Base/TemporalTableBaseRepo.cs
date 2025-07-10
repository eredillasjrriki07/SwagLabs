using Microsoft.EntityFrameworkCore;
using SwagLabs.Dal.EfStructures;
using SwagLabs.Dal.Repos.Base;
using SwagLabs.Models.Entities.Base;
using SwagLabs.Models.Models;

public abstract class TemporalTableBaseRepo<T> : BaseRepo<T>, ITemporalTableBaseRepo<T> where T : BaseEntity, new()
{
    protected TemporalTableBaseRepo(AppDbContext context) : base(context) { }
    protected TemporalTableBaseRepo(DbContextOptions<AppDbContext> options) : this(new AppDbContext(options)) { }

    public async Task<IEnumerable<TemporalViewModel<T>>> GetAllHistoryAsync()
        => await ExecuteQueryAsync(Table.TemporalAll());

    public async Task<IEnumerable<TemporalViewModel<T>>> GetHistoryAsOfAsync(DateTime dateTime)
        => await ExecuteQueryAsync(Table.TemporalAsOf(ConvertToUTC(dateTime)));

    public async Task<IEnumerable<TemporalViewModel<T>>> GetHistoryBetweenAsync(DateTime startDateTime, DateTime endDateTime)
        => await ExecuteQueryAsync(Table.TemporalBetween(ConvertToUTC(startDateTime), ConvertToUTC(endDateTime)));

    public async Task<IEnumerable<TemporalViewModel<T>>> GetHistoryContainedInAsync(DateTime startDateTime, DateTime endDateTime)
        => await ExecuteQueryAsync(Table.TemporalContainedIn(ConvertToUTC(startDateTime), ConvertToUTC(endDateTime)));

    public async Task<IEnumerable<TemporalViewModel<T>>> GetHistoryFromToAsync(DateTime startDateTime, DateTime endDateTime)
        => await ExecuteQueryAsync(Table.TemporalFromTo(ConvertToUTC(startDateTime), ConvertToUTC(endDateTime)));

    internal static DateTime ConvertToUTC(DateTime dateTime)
        => TimeZoneInfo.ConvertTimeToUtc(dateTime, TimeZoneInfo.Local);

    internal static async Task<IEnumerable<TemporalViewModel<T>>> ExecuteQueryAsync(IQueryable<T> query)
    {
        return await query.OrderBy(e => EF.Property<DateTime>(e, "ValidFrom"))
                .Select(e => new TemporalViewModel<T>
                {
                    Entity = e,
                    ValidFrom = EF.Property<DateTime>(e, "ValidFrom"),
                    ValidTo = EF.Property<DateTime>(e, "ValidTo")
                })
                .ToListAsync();
    }
}