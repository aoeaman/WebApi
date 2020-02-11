using System;
using System.Collections.Generic;
using System.IO;
using CarPoolApplication.Models;
using Newtonsoft.Json;
using CodeFirst.Services.Interfaces;
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
            _context.SaveChanges();
        }

        public Driver Create(Driver driver)
        {
            driver.ID = Service.GenerateID();
            return driver;
        }

        public void Delete(int iD)
        {
            throw new NotImplementedException();
        }

        public List<Driver> GetAll()
        {
            return _context.Drivers.ToList();
        }

        public Driver GetByID(int id)
        {
            return _context.Drivers.Find(id);
        }
    }
}
