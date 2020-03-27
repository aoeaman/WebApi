using CarPool.Application.Models;
using System;
using System.Collections.Generic;

namespace CarPool.Services.Contracts
{
    public interface IUserService : IBaseService<User>
    {
        string Authenticate(string username, string password);
    }
}
