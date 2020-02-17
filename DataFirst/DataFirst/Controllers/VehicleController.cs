using CarPoolApplication.Concerns;
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
        [Authorize(Roles = Role.User)]
        [Route("Create")]
        [HttpPost]
        public string Create([FromBody] Vehicle vehicle)
        {
            return _repos.Add(vehicle).Response.ReasonPhrase;
        }
        [Authorize(Roles = Role.Admin)]
        [Route("GetAll")]
        [HttpGet]
        public List<Vehicle> GetAll()
        {
            return _repos.GetAll();
        }

        [Authorize]
        [Route("{id:int}")]
        [HttpGet]
        public Vehicle GetByID(int id)
        {
            return _repos.GetByID(id);
        }
        [Authorize(Roles = Role.SuperUser)]
        [Route("Disable/{id:int}")]
        [HttpPut]
        public string Disable(int id)
        {
            return _repos.Disable(id).Response.ReasonPhrase;
        }
        [Authorize(Roles = Role.User)]
        [Route("Delete/{id:int}")]
        [HttpDelete]
        public string Delete(int id)
        {
            return _repos.Delete(id).Response.ReasonPhrase;
        }
    }
}
