using SwagLabs.Models.Entities;

namespace SwagLabs.Services.ApiWrapper.Services.Interfaces;

public interface IUserApiServiceWrapper : IApiServiceWrapperBase<User>
{
    Task<User> FindByUsernameAsync(string username);
}
