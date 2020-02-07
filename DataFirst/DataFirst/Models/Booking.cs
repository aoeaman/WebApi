using System.ComponentModel.DataAnnotations;

namespace CarPoolApplication.Models
{
    public class Booking
    {
        [Key]
        public int ID { get; set; }
        public StatusOfRide Status { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
        public virtual Rider Rider { get; set; }
        public virtual Offer Offer { get; set; }
        public float Fare { get; set; }
        public byte Seats { get; set; }
    }
}
