using CarPoolApplication.Concerns;
using CarPoolApplication.Helpers;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace CodeFirst.Controllers
{
    [Authorize(Roles =Role.User)]
    [Route("api/[Controller]")]
    public class UserController :Controller
    {
        private readonly IUserService _repos;
        public UserController(IUserService repos)
        {
            _repos = repos;
        }

        [Route("Signup")]
        [AllowAnonymous]
        [HttpPost]       
        public IActionResult Create([FromBody] User Model)
        {
            var user = _repos.Add(Model);
            if (user == null)
                return BadRequest(new { message = "Username Already Exists" });

            return Ok(new { message = "Successfully Created with ID = "+ user.ID });
            
        }

        [HttpPost("Login")]
        [AllowAnonymous]
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

        [Route("{id}")]
        [HttpGet]
        public User GetByID(int id)
        {
            return _repos.GetByID(id);
        }

        [Route("delete/{id}")]
        [HttpPut]
        public string Delete(int id)
        {
            return _repos.Delete(id);
        }
    }
}
