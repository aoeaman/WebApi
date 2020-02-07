using System;
using System.Collections.Generic;
using System.IO;
using CarPoolApplication.Models;
using Newtonsoft.Json;
using CarPoolApplication.Services.Interfaces;
using CodeFirst.Models;
using System.Linq;

namespace CarPoolApplication.Services
{
    public class DriverService:IService<Driver>
    {
        UtilityService Service;
        Context _context;
        public DriverService(Context context)
        {
            Service = new UtilityService();
            _context = context;
        }

        public void Add(Driver driver)
        {
            _context.Drivers.Add(driver);
        }

        public Driver Create(Driver driver)
        {
            driver.ID = Service.GenerateID();
            return driver;
        }

        public List<Driver> GetAll()
        {
            return _context.Drivers.ToList();
        }

    }
}
