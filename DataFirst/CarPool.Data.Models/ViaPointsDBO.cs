using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPool.Data.Models
{ 
    public class ViaPointsDBO
    {
        [Key]
        public int ID { get; set; }
        public int OfferID { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(10)")]
        public Cities City { get; set; }
        [Required]
        public virtual OfferDBO Offer { get; set; }
    }
}
