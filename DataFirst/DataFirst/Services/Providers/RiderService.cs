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
        readonly UtilityService Util;
        private readonly IServiceScope _scope;
        
        public RiderService(IServiceProvider service)
        {
            _scope = service.CreateScope();
            Util = new UtilityService();           
        }

        public Rider Create(Rider rider)
        {
            rider.ID = Util.GenerateID();
            return rider;
        }

        public void Add(Rider rider)
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
        public void Delete(int iD)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            _context.Riders.Remove(_context.Riders.Find(iD));
        }

        HttpResponseException IBaseService<Rider>.Add(Rider entity)
        {
            throw new NotImplementedException();
        }

        HttpResponseException IBaseService<Rider>.Delete(int iD)
        {
            throw new NotImplementedException();
        }
    }
}
