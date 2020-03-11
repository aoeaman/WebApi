using System;
using System.Collections.Generic;
using System.Linq;
using CarPool.Helpers;
using CarPool.Services.Contracts;
using CarPool.Data.Models;
using CarPool.Application.Models;
using AutoMapper;
using CodeFirst;

namespace CarPool.Services.Providers
{
    public class VehicleService:IVehicleService
    {
        readonly Context _context;
        private readonly IMapper _mapper;

        public VehicleService(Context context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public Vehicle Add(Vehicle vehicle)
        {
            VehicleDBO _vehicle = _mapper.Map<VehicleDBO>(vehicle);
            try
            {   
                _vehicle.IsActive = true;             
                _context.Vehicles.Add(_vehicle);
                _context.SaveChanges();
                return _mapper.Map<Vehicle>(_vehicle);

            }
            catch (Exception)
            {
                _context.Vehicles.Remove(_vehicle);
                return null;
            }
        }
        public string Delete(int id)
        {
            try
            {
                _context.Vehicles.Remove(_mapper.Map<VehicleDBO>(GetByID(id)));
                return Status.Ok.ToString();
            }
            catch (Exception)
            {
                return Status.NotFound.ToString();
            }
            
        }

        public List<Vehicle> GetAll()
        {
            try
            {
                List<Vehicle> Vehicles = new List<Vehicle>();
                foreach (var vehicle in _context.Vehicles)
                {
                    Vehicles.Add(_mapper.Map<Vehicle>(vehicle));
                }
                return Vehicles;
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
                return _mapper.Map<Vehicle>(_context.Vehicles.Find(id));
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
