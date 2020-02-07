using System.ComponentModel.DataAnnotations;

namespace CarPoolApplication.Models
{
    public class ViaPoints
    {
        [Key]
        public int ID { get; set; }      
        public Cities City { get; set; }
        public virtual Offer Offer { get; set; }
    }
}
