﻿using CarPool.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using CarPool.Application.Models;
using CarPool.Helpers;

namespace CodeFirst.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    public class VehicleController:Controller
    {
        private readonly IVehicleService _repos;
        public VehicleController(IVehicleService repos)
        {
            _repos = repos;
        }
        
        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody] Vehicle Model)
        {
            var vehicle = _repos.Add(Model);
            if (vehicle == null)
            {
                return BadRequest(new { message = "Error Occured" });
            }
            return Ok(new { message =vehicle.ID });
        }
        [Authorize(Roles = Role.Admin)]
        [Route("GetAll")]
        [HttpGet]
        public List<Vehicle> GetAll()
        {
            
            return _repos.GetAll(); ;
        }

        [Route("{id:int}")]
        [HttpGet]
        public Vehicle GetByID(int id)
        {
            return _repos.GetByID(id);
        }

        [Route("Disable/{id:int}")]
        [HttpPut]
        public string Disable(int id)
        {
            return _repos.Disable(id);
        }

        [Route("Delete/{id:int}")]
        [HttpDelete]
        public string Delete(int id)
        {
            return _repos.Delete(id);
        }
    }
}
