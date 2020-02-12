using System;
using System.Collections.Generic;
using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using CodeFirst.Models;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace CarPoolApplication.Services
{
    public class DriverService : IDriverService,IDisposable
    {
        UtilityService Util;
        readonly IServiceScope _scope;
        public DriverService(IServiceProvider service)
        {
            Util = new UtilityService();           
            _scope = service.CreateScope();
        }
        public void Add(Driver driver)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            _context.Drivers.Add(driver);
            _context.SaveChanges();

        }

        public Driver Create(Driver driver)
        {
            driver.ID = Util.GenerateID();
            return driver;
        }

        public void Delete(int iD)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            _context.Drivers.Remove(_context.Drivers.Find(iD));
        }

        public void Dispose()
        {
           _scope?.Dispose();
        }

        public List<Driver> GetAll()
        {
             var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            return _context.Drivers.ToList();
        }

        public Driver GetByID(int id)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            return _context.Drivers.Find(id);
        }
    }
}
