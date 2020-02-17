using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CarPoolApplication.Concerns;

namespace CodeFirst.Services.Interfaces
{
    public interface IUserService : IBaseService<User>
    {
       User Authenticate(string username, string password);
    }
}
