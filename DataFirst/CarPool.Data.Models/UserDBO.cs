using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarPool.Data.Models
{
    public class UserDBO
    {
        public UserDBO()
        {
            Bookings = new HashSet<BookingDBO>();

            Offers = new HashSet<OfferDBO>();

            Vehicles = new HashSet<VehicleDBO>();
        }
    
        [Key]
        public int ID { get; set; }       
        public string Name { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
        public char Gender { get; set; }
        public byte Age { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string DrivingLiscenceNumber { get; set; }
        public bool IsActive { get; set; }
        public virtual ICollection<BookingDBO> Bookings { get; set; }
        public virtual ICollection<OfferDBO> Offers { get; set; }
        public virtual ICollection<VehicleDBO> Vehicles { get; set; }

        public string Role { get; set; }
        public string Token { get; set; }
    }
}
