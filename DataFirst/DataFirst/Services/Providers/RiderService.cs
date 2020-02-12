using System.Collections.Generic;
using CarPoolApplication.Models;
using CodeFirst.Models;
using System.Linq;
using CodeFirst.Services.Interfaces;
using System;
using Microsoft.Extensions.DependencyInjection;

namespace CarPoolApplication.Services
{
    public class RiderService : IRiderService
    {
        readonly UtilityService Util;
        private readonly IServiceScope _scope;
        public static int Count { get; set; }
        

        Context _context;
        public RiderService(IServiceProvider service)
        {
            Count++;
            _scope = service.CreateScope();
            Util = new UtilityService();
            _context = _scope.ServiceProvider.GetRequiredService<Context>();
        }

        public Rider Create(Rider rider)
        {
            rider.ID = Util.GenerateID();
            return rider;
        }

        public void Add(Rider rider)
        {
            
            _context.Riders.Add(rider);
            _context.SaveChanges();

        }
        public List<Rider> GetAll()
        {
            return _context.Riders.ToList();
        }
        public Rider GetByID(int id)
        {
            return _context.Riders.Find(id);
        }
        public void Delete(int iD)
        {
             _context.Riders.Remove(_context.Riders.Find(iD));
        }

        public int count()
        {
            return Count;
        }
    }
}
