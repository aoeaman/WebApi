using CarPoolApplication.Models;
using CarPoolApplication.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Controllers
{
    public class DriverController
    {
        private IService<Driver> _repos;
        public DriverController(IService<Driver> repos)
        {
            _repos = repos;
        }
    }
}
