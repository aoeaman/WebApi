using System.Collections.Generic;
using System.IO;
using CarPoolApplication.Models;
using CodeFirst.Models;
using Newtonsoft.Json;
using System.Linq;

namespace CarPoolApplication.Services
{
    public class RiderService:IService<Rider>
    {
        UtilityService Service;
        
        Context _context;
        public RiderService(Context context)
        {
            Service = new UtilityService();
            _context = context;
        }

        public Rider Create(Rider rider)
        {              
            rider.ID = Service.GenerateID();
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

        public void SaveData()
        {

        }

    }
}
