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
    [Route("Rider")]
    public class RiderController : ControllerBase
    {
        private IRiderService _repos;
        
        public RiderController(IRiderService repos)
        {
            _repos = repos;
        }
        [Route("Create")]
        [HttpPost]
        public string Create([FromBody] Rider rider)
        {
            if (!ModelState.IsValid)
                return "Bad";
            _repos.Add(_repos.Create(rider));
            return "Ok";
        }

        [Route("GetAll")]
        [HttpGet]
        public List<Rider> GetAll()
        {
            return _repos.GetAll();
        }

        [Route("{id:int}")]
        [HttpGet]
        public Rider GetByID(int id)
        {
            return _repos.GetByID(id);
        }

        [Route("cnt")]
        [HttpGet]
        public int GetByID()
        {
            return _repos.count();
        }
    }
}
