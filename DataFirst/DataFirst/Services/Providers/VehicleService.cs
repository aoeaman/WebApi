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
            Context _context;
            try
            {
                _context = _scope.ServiceProvider.GetRequiredService<Context>();
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.BadGateway);
            }
            try
            {               
                vehicle.IsActive = true;
                _context.Vehicles.Add(vehicle);
                _context.SaveChanges();
                return new HttpResponseException(System.Net.HttpStatusCode.Created);

            }
            catch (Exception)
            {
                _context.Vehicles.Remove(vehicle);
                return new HttpResponseException(System.Net.HttpStatusCode.Conflict);
            }
        }
        public HttpResponseException Delete(int id)
        {
            Context _context;
            try
            {
                _context = _scope.ServiceProvider.GetRequiredService<Context>();
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.BadGateway);
            }
            try
            {
                _context.Vehicles.Remove(GetByID(id));
                return new HttpResponseException(System.Net.HttpStatusCode.OK);
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
            
        }

        public List<Vehicle> GetAll()
        {
            try
            {
                return _scope.ServiceProvider.GetRequiredService<Context>().Vehicles.ToList();
            }
            catch
            {             
                return null;           
            }
        }
        public Vehicle GetByID(int id)
        {
            try
            { 
            return _scope.ServiceProvider.GetRequiredService<Context>().Vehicles.Find(id);
            }
            catch (Exception)
            {
                return null;
            }
        }
        public HttpResponseException Disable(int id)
        {
            Context _context;
            try
            {
                _context = _scope.ServiceProvider.GetRequiredService<Context>();
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.BadGateway);
            }
            try
            {
                var vehicle = _context.Vehicles.Find(id);
                if (vehicle.IsActive)
                {
                    if (_context.Offers.Any(_ => _.VehicleID == id))
                    {
                        return new HttpResponseException(System.Net.HttpStatusCode.BadRequest);
                    }
                    else
                    {
                        vehicle.IsActive = false;
                        return new HttpResponseException(System.Net.HttpStatusCode.OK);
                    }
                }
                else
                {
                    return new HttpResponseException(System.Net.HttpStatusCode.Conflict);
                }
            }
            catch (Exception)
            {
                return new HttpResponseException(System.Net.HttpStatusCode.NotFound);
            }
        }
    }
}
