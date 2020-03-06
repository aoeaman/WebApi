using System;
using System.Collections.Generic;
using System.Linq;
using CarPool.Helpers;
using CarPool.Services.Contracts;
using CarPool.Data.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CarPool.Services.Providers
{
    public class VehicleService:IVehicleService
    {
        private readonly IServiceScope _scope;
        readonly Context _context;
        public VehicleService(IServiceProvider service)
        {
            _scope = service.CreateScope();
            _context = _scope.ServiceProvider.GetRequiredService<Context>();
        }
        public VehicleDBO Add(VehicleDBO vehicle)
        {          
            try
            {               
                vehicle.IsActive = true;
                _context.Vehicles.Add(vehicle);
                _context.SaveChanges();
                return vehicle;

            }
            catch (Exception)
            {
                _context.Vehicles.Remove(vehicle);
                return null;
            }
        }
        public string Delete(int id)
        {
            try
            {
                _context.Vehicles.Remove(GetByID(id));
                return Status.Ok.ToString();
            }
            catch (Exception)
            {
                return Status.NotFound.ToString();
            }
            
        }

        public List<VehicleDBO> GetAll()
        {
            try
            {
                return _context.Vehicles.ToList();
            }
            catch
            {             
                return null;           
            }
        }
        public VehicleDBO GetByID(int id)
        {
            try
            { 
            return _context.Vehicles.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public string Disable(int id)
        {
            try
            {
                var vehicle = _context.Vehicles.Find(id);
                if (vehicle.IsActive)
                {
                    if (_context.Offers.Any(v => v.VehicleID == id))
                    {
                        return Status.Failed.ToString();
                    }
                    else
                    {
                        vehicle.IsActive = false;
                        return Status.Ok.ToString();
                    }
                }
                else
                {
                    return Status.Failed.ToString();
                }
            }
            catch (Exception)
            {
                return Status.NotFound.ToString();
            }
        }
    }
}
