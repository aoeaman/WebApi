using System.Collections.Generic;

namespace CarPoolApplication.Models
{
    public class Rider:User
    {
        public Rider()
        {
            Bookings = new HashSet<Booking>();
        }

        public virtual ICollection<Booking> Bookings { get; set; }
    }
}
