using CarPoolApplication.Models;
using CarPoolApplication.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Controllers
{
    public class RiderController : ControllerBase
    {
        private IService<Rider> _repos;
        public RiderController(IService<Rider> repos)
        {
            _repos = repos;
        }

        
    }
}
