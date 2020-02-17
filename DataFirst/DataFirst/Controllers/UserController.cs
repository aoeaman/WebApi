using CarPoolApplication.Concerns;
using CarPoolApplication.Helpers;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace CodeFirst.Controllers
{

    [Route("api/[Controller]")]
    [Authorize]
    public class UserController :Controller
    {
        private IUserService _repos;
        public UserController(IUserService repos)
        {
            _repos = repos;
        }

        [AllowAnonymous]
        [Route("Signup")]
        [HttpPost]       
        public string Create([FromBody] User user)
        {
            return _repos.Add(user).Response.ReasonPhrase;
        }
        [AllowAnonymous]
        [HttpPost("Login")]
        public IActionResult Authenticate([FromBody]Login model)
        {
            var user = _repos.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [Route("GetAll")]
        [Authorize(Roles = Role.Admin)]
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
