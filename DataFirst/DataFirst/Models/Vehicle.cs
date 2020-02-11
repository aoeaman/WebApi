using System.ComponentModel.DataAnnotations;

namespace CarPoolApplication.Models
{
    public class Vehicle
    {
        [Key]
        public int ID { get; set; }
        public int DriverID { get; set; }
        public string Maker { get; set; }
        public string Number { get; set; }
        public byte Seats { get; set; }
        public bool IsActive { get; set; }
        [Required]
        public VehicleType Type { get; set; }

        [Required]
        public virtual Driver Driver { get; set; }
    }
}
