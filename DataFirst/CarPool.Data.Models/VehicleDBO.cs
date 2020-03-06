using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace CarPool.Data.Models
{
    public class VehicleDBO
    {
        public VehicleDBO()
        {
            Offers = new HashSet<OfferDBO>();
        }

        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public string Maker { get; set; }
        public string Number { get; set; }
        public byte Seats { get; set; }
        public bool IsActive { get; set; }
        public VehicleType Type { get; set; }      
        public virtual UserDBO User { get; set; }
        public virtual ICollection<OfferDBO> Offers { get; set; }
    }
}
