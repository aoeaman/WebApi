using CarPool.Data.Models;
using System.Collections.Generic;

namespace CarPool.Application.Models
{
    public class Offer
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int VehicleID { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
        public string StartDate { get; set; }
        public string EndDate { get; set; }
        public int SeatsAvailable { get; set; }
        public List<ViaPoints> ViaPoints { get; set; }      
    }
}
