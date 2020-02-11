using CarPoolApplication.Models;
using CarPoolApplication.Services;
using CodeFirst.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace CodeFirst.Controllers
{
    [Route("Driver")]
    public class DriverController : ControllerBase
    {
        private IService<Driver> _repos;
        public DriverController(IService<Driver> repos)
        {
            _repos = repos;
        }

        [Route("Create")]
        [HttpPost]
        public string PostDriver([FromBody] Driver driver)
        {
            if (!ModelState.IsValid)
                return "Bad";
            _repos.Add(_repos.Create(driver));
            return "Ok";
        }
        [Route("GetAll")]
        [HttpGet]
        public List<Driver> GetAll()
        {
            return _repos.GetAll();
        }

        [Route("GetByID/{id:int}")]
        [HttpGet]
        public Driver GetByID(int id)
        {
            return _repos.GetByID(id);
        }
    }
}
