using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        public string DrivingLiscenceNumber { get; set; }
    }
}
