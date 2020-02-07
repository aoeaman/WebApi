using CarPoolApplication.Models;
using CarPoolApplication.Services;
using CarPoolApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeFirst.Controllers
{
    public class VehicleController
    {
        private IVehicleService _repos;
        public VehicleController(IVehicleService repos)
        {
            _repos = repos;
        }
    }
}
