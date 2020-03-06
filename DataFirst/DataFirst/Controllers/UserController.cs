using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CarPool.Services.Contracts;
using CarPool.Data.Models;
using CarPool.Helpers;
using AutoMapper;
using CarPool.Application.Models;

namespace CodeFirst.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    public class UserController :Controller
    {
        private readonly IUserService _repos;
        private readonly IMapper _mapper;
        public UserController(IUserService repos,IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }

        [Route("Signup")]
        [AllowAnonymous]
        [HttpPost]       
        public IActionResult Create([FromBody] User Model)
        {
            var UserDbo = _mapper.Map<UserDBO>(Model);
            var user = _repos.Add(UserDbo);
            if (user == null)
                return BadRequest(new { message = "Username Already Exists" });

            return Ok(new { message = + user.ID });
            
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
        [Authorize (Roles =Role.Admin)]
        [Route("GetAll")]
        [HttpGet]
        public List<User> GetAll()
        {           
            List<User> Users = new List<User>();
            foreach(var user in _repos.GetAll())
            {
                Users.Add(_mapper.Map<User>(user));
            }
            return Users;
        }

        [Route("{id}")]
        [HttpGet]
        public User GetByID(int id)
        {
            return _mapper.Map<User>(_repos.GetByID(id));
        }

        [Route("delete/{id}")]
        [HttpPut]
        public string Delete(int id)
        {
            return _repos.Delete(id);
        }
    }
}
