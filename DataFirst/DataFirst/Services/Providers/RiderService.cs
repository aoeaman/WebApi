using System.Collections.Generic;
using CarPoolApplication.Models;
using CodeFirst.Models;
using System.Linq;
using CodeFirst.Services.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;
using System.Web.Http;

namespace CarPoolApplication.Services
{
    public class RiderService : IRiderService
    {
        private readonly IServiceScope _scope;
        
        public RiderService(IServiceProvider service)
        {
            _scope = service.CreateScope();           
        }
        public HttpResponseException Add(Rider rider)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            _context.Riders.Add(rider);
            _context.SaveChanges();

        }
        public List<Rider> GetAll()
        {
            return _scope.ServiceProvider.GetRequiredService<Context>().Riders.ToList();
        }
        public Rider GetByID(int id)
        {
            return _scope.ServiceProvider.GetRequiredService<Context>().Riders.Find(id);
        }
        public HttpResponseException Delete(int iD)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            _context.Riders.Remove(_context.Riders.Find(iD));
        }

    }
}
