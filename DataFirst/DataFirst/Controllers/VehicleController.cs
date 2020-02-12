using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeFirst.Controllers
{
    [Route("Vehicle")]
    public class VehicleController:ControllerBase
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
            if (!ModelState.IsValid)
                return "Bad";
            _repos.Add(_repos.Create(vehicle));
            return "Ok";
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
    }
}
