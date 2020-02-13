using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using System.Collections.Generic;
using System.Web.Http;

namespace CodeFirst.Controllers
{
    [Route("Driver")]
    public class DriverController : Microsoft.AspNetCore.Mvc.Controller
    {
        private IDriverService _repos;
        public DriverController(IDriverService repos)
        {
            _repos = repos;
        }

        [Route("Create")]
        [HttpPost]
        public HttpResponseException PostDriver([FromBody] Driver driver)
        {
            return _repos.Add(driver);
        }
        [Route("GetAll")]
        [HttpGet]
        public List<Driver> GetAll()
        {
            return _repos.GetAll();
        }

        [Route("{id:int}")]
        [HttpGet]
        public Driver GetByID(int id)
        {
            return _repos.GetByID(id);
        }
    }
}
