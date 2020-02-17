using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolApplication.Concerns
{
    public class User
    {
        public User()
        {
            Bookings = new HashSet<Booking>();

            Offers = new HashSet<Offer>();

            Vehicles = new HashSet<Vehicle>();
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
        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Offer> Offers { get; set; }
        public virtual ICollection<Vehicle> Vehicles { get; set; }

        public string Role { get; set; }
        public string Token { get; set; }
    }
}
