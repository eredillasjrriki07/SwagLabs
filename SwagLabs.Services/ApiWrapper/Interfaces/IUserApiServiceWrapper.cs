
using SwagLabs.Models.Entities;

namespace SwagLabs.Services.ApiWrapper.Interfaces;

public interface IUserApiServiceWrapper : IApiServiceWrapperBase<User>
{
    Task<User> LoginWithUserandPasswordAsync(string username, string password);
}
