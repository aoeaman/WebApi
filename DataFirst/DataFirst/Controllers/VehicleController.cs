using CarPoolApplication;
using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeFirst.Controllers
{
    [Route("api/[Controller]")]
    [Authorize]
    public class VehicleController:Controller
    {
        private IVehicleService _repos;
        public VehicleController(IVehicleService repos)
        {
            _repos = repos;
        }

        [Route("Create")]
        [HttpPost]
        public string Create([FromBody] Vehicle vehicle)
        {
            return _repos.Add(vehicle).Response.ReasonPhrase;
        }

        [Route("GetAll")]
        [HttpGet]
        public List<Vehicle> GetAll()
        {
            return _repos.GetAll();
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
            return _repos.Disable(id).Response.ReasonPhrase;
        }
        [Route("Delete/{id:int}")]
        [HttpDelete]
        public string Delete(int id)
        {
            return _repos.Delete(id).Response.ReasonPhrase;
        }
    }
}
