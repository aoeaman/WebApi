namespace CarPool.Application.Models
{ 
    public class Booking
    {
        public int ID { get; set; }
        public int Status { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
        public int OfferID { get; set; }
        public int UserID { get; set; }       
        public float Fare { get; set; }
        public byte Seats { get; set; }
    }
}
