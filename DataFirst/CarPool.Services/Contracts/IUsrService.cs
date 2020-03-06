using CarPool.Data.Models;

namespace CarPool.Services.Contracts
{
    public interface IUserService : IBaseService<UserDBO>
    {
       UserDBO Authenticate(string username, string password);
    }
}
