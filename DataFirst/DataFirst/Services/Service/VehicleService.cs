using System.Collections.Generic;
using System.Linq;
using CarPoolApplication.Models;
using CodeFirst.Models;
using CodeFirst.Services.Interfaces;

namespace CarPoolApplication.Services
{
    public class VehicleService:IVehicleService
    {
        UtilityService Service;
        Context _context;
        public VehicleService(Context context)
        {
            Service = new UtilityService();
            _context = context;
        }

        public void Add(Vehicle vehicle)
        {
            _context.Vehicles.Add(vehicle);
            _context.SaveChanges();
        }
        public Vehicle Create(Vehicle vehicle)
        {
            vehicle.ID = Service.GenerateID();
            return vehicle;
        }

        public void Delete(int iD)
        {
            throw new System.NotImplementedException();
        }

        public List<Vehicle> GetAll()
        {
            return _context.Vehicles.ToList();
        }
        public Vehicle GetByID(int iD)
        {
            return _context.Vehicles.Find(iD);
        }
    }
}
