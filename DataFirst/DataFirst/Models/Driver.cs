using System.Collections.Generic;

namespace CarPoolApplication.Models
{
    public class Driver :User
    {
        public Driver()
        {
            Offers = new HashSet<Offer>();

            Vehicles = new HashSet<Vehicle>();
        }

        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }
        public string DrivingLiscenceNumber { get; set; }
    }
}
