namespace CarPool.Application.Models
{ 
    public class Vehicle
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Maker { get; set; }
        public string Number { get; set; }
        public int Seats { get; set; }
        public bool IsActive { get; set; }
        public int Type { get; set; }      
    }
}
