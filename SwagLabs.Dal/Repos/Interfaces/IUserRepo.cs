
using SwagLabs.Dal.Repos.Base;
using SwagLabs.Models.Entities;

namespace SwagLabs.Dal.Repos.Interfaces;

public interface IUserRepo : ITemporalTableBaseRepo<User>
{
    Task<User> FindByUsernameAsync(string username);
}
