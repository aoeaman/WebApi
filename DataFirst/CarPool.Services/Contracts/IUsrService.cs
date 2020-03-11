using CarPool.Application.Models;

namespace CarPool.Services.Contracts
{
    public interface IUserService : IBaseService<User>
    {
       User Authenticate(string username, string password);
    }
}
