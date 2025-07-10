
using SwagLabs.Models.Entities.Base;
using SwagLabs.Models.Models;

namespace SwagLabs.Dal.Repos.Base;

public interface ITemporalTableBaseRepo<T> : IBaseRepo<T> where T : BaseEntity, new()
{
    Task<IEnumerable<TemporalViewModel<T>>> GetAllHistoryAsync();
    Task<IEnumerable<TemporalViewModel<T>>> GetHistoryAsOfAsync(DateTime dateTime);
    Task<IEnumerable<TemporalViewModel<T>>> GetHistoryBetweenAsync(DateTime startDateTime, DateTime endDateTime);
    Task<IEnumerable<TemporalViewModel<T>>> GetHistoryContainedInAsync(DateTime startDateTime, DateTime endDateTime);
    Task<IEnumerable<TemporalViewModel<T>>> GetHistoryFromToAsync(DateTime startDateTime, DateTime endDateTime);
}
