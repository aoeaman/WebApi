using CarPoolApplication.Models;
using CarPoolApplication.Services;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public void Create(Vehicle vehicle)
        {
            _repos.Add(_repos.Create(vehicle));
        }

        [Route("GetAll")]
        [HttpGet]
        public List<Vehicle> GetAll()
        {
            return _repos.GetAll();
        }

        [Route("GetByID/{id:int}")]
        [HttpGet]
        public Vehicle GetByID(int id)
        {
            return _repos.GetByID(id);
        }
    }
}
