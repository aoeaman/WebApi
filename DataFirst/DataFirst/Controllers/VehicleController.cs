using CarPool.Services.Contracts;
using CarPool.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using AutoMapper;
using CarPool.Application.Models;

namespace CodeFirst.Controllers
{
    [Authorize]
    [Route("api/[Controller]")]
    public class VehicleController:Controller
    {
        private readonly IVehicleService _repos;
        private readonly IMapper _mapper;
        public VehicleController(IVehicleService repos,IMapper mapper)
        {
            _repos = repos;
            _mapper = mapper;
        }
        
        [Route("Create")]
        [HttpPost]
        public IActionResult Create([FromBody] Vehicle Model)
        {
            var vehicle = _repos.Add(_mapper.Map<VehicleDBO>(Model));
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
            List<Vehicle> Vehicles = new List<Vehicle>();
            foreach(var vehicle in _repos.GetAll())
            {
                Vehicles.Add(_mapper.Map<Vehicle>(vehicle));
            }
            return Vehicles;
        }

        [Route("{id:int}")]
        [HttpGet]
        public Vehicle GetByID(int id)
        {
            return _mapper.Map<Vehicle>(_repos.GetByID(id));
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
