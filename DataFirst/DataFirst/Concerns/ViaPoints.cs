using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolApplication.Concerns
{
    public class ViaPoints
    {
        [Key]
        public int ID { get; set; }
        public int OfferID { get; set; }
        [Required]
        [Column(TypeName ="nvarchar(10)")]
        public Cities City { get; set; }
        [Required]
        public virtual Offer Offer { get; set; }
    }
}
