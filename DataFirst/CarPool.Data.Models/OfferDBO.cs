using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPool.Data.Models
{
    public class OfferDBO
    {
        public OfferDBO()
        {
            ViaPoints = new HashSet<ViaPointsDBO>();
        }

        [Key]
        public int ID { get; set; }
        public int UserID { get; set; }
        public int VehicleID { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public StatusOfRide Status { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(15)")]
        public Cities Source { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(15)")]
        public Cities Destination { get; set; }
        [Column(TypeName = "nvarchar(15)")]
        public Cities CurrentLocaton { get; set; }       
        public virtual UserDBO User { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public byte SeatsAvailable { get; set; }
        public bool IsActive { get; set; }
        public float Earnings { get; set; }
        [Required]
        public virtual ICollection< ViaPointsDBO> ViaPoints { get; set; }      
    }
}
