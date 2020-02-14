using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolApplication.Models
{
    public class Vehicle
    {
        [Key]
        [Column(Order = 1)]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Maker { get; set; }
        public string Number { get; set; }
        public byte Seats { get; set; }
        public bool IsActive { get; set; }
        public VehicleType Type { get; set; }      
        public virtual User User { get; set; }
        public virtual IList<Offer> Offers { get; set; }
    }
}
