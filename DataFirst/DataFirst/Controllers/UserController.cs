using CarPoolApplication;
using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
namespace CodeFirst.Controllers
{
    
    [Route("api/[Controller]")]
    [BasicAuthentication]
    public class UserController :Controller
    {
        private IUserService _repos;
        public UserController(IUserService repos)
        {
            _repos = repos;
        }

        [Route("Create")]
        [HttpPost]
        public string Create([FromBody] User user)
        {
            return _repos.Add(user).Response.ReasonPhrase;
        }
        [Route("GetAll")]
        [HttpGet]
        public List<User> GetAll()
        {
            return _repos.GetAll();
        }

        [Route("{id:int}")]
        [HttpGet]
        public User GetByID(int id)
        {
            return _repos.GetByID(id);
        }
    }
}
