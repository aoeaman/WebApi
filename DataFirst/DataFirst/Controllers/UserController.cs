using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using CarPool.Services.Contracts;
using CarPool.Helpers;
using AutoMapper;
using CarPool.Application.Models;
using System;

namespace CodeFirst.Controllers
{
    [Authorize]
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

            return Ok(new { message = + user.ID });
            
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public IActionResult Authenticate([FromBody]Login model)
        {
            string user =  _repos.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(user);

            return Ok(user);
        }
        [Authorize (Roles =Role.Admin)]
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

        [Route("delete/{id:int}")]
        [HttpPut]
        public string Delete(int id)
        {
            return _repos.Delete(id);
        }
    }
}
