using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using CarPoolApplication.Models;
using CodeFirst.Models;
using CodeFirst.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace CarPoolApplication.Services
{
    public class VehicleService:IVehicleService
    {
        private readonly IServiceScope _scope;
        public VehicleService(IServiceProvider service)
        {
            _scope = service.CreateScope();
        }

        public HttpResponseException Add(Vehicle vehicle)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }

        public HttpResponseException Delete(int iD)
        {
            _scope.ServiceProvider.GetRequiredService<Context>().Vehicles.Remove(GetByID(iD));
        }

        public List<Vehicle> GetAll()
        {
            return _scope.ServiceProvider.GetRequiredService<Context>().Vehicles.ToList();
        }
        public Vehicle GetByID(int iD) 
        {
            return _scope.ServiceProvider.GetRequiredService<Context>().Vehicles.Find(iD);
        }

        public string Disable(int id)
        {
            var _context = _scope.ServiceProvider.GetRequiredService<Context>();
            var vehicle = _context.Vehicles.Find(id);
            if (vehicle.IsActive)
            {
                if(_context.Offers.Any(_=> _.VehicleID == id))
                {
                    return $"Cannot Diable due to Active Offer";
                }
                else
                {
                    vehicle.IsActive = false;
                    return $"Disabled";
                }
            }
            else
            {
                return $"Already Disabled";
            }
        }

    }
}
