using System;
using System.Collections.Generic;
using CarPoolApplication.Models;
using CodeFirst.Services.Interfaces;
using CodeFirst.Models;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http;

namespace CarPoolApplication.Services
{
    public class DriverService : IDriverService
    {
        readonly IServiceScope _scope;
        public DriverService(IServiceProvider service)
        {          
            _scope = service.CreateScope();
        }
        public HttpResponseException Add(Driver driver)
        {
            try
            {
                var _context = _scope.ServiceProvider.GetRequiredService<Context>();
                _context.Drivers.Add(driver);
                _context.SaveChanges();
                return new HttpResponseException(System.Net.HttpStatusCode.Created);
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.BadRequest );
            }
            

        }

        public HttpResponseException Delete(int iD)
        {
            try
            {
                var _context = _scope.ServiceProvider.GetRequiredService<Context>();
                _context.Drivers.Remove(_context.Drivers.Find(iD));
                return new HttpResponseException(System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
            
        }
        public List<Driver> GetAll()
        {
            try
            {
                return _scope.ServiceProvider.GetRequiredService<Context>().Drivers.ToList();
            }
            catch (Exception)
            {
                return null;
            }
            
        }

        public Driver GetByID(int id)
        {
            try
            {
                return _scope.ServiceProvider.GetRequiredService<Context>().Drivers.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
            
        }
    }
}
