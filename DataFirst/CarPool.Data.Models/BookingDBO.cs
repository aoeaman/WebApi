using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPool.Data.Models
{
    public class BookingDBO
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public StatusOfRide Status { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        [Required]
        public Cities Source { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        public Cities Destination { get; set; }
        public int OfferID { get; set; }
        public int UserID { get; set; }       
        [Required]
        public float Fare { get; set; }
        [Required]
        public byte Seats { get; set; }
        public bool IsActive { get; set; }
        public virtual UserDBO User { get; set; }
        public virtual OfferDBO Offer { get; set; }
    }
}
