using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolApplication.Models
{
    public class Offer
    {
        [Key]
        public int ID { get; set; }
        public int DriverID { get; set; }
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
        public virtual Driver Driver { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        [Required]
        public byte SeatsAvailable { get; set; }
        public float Earnings { get; set; }
        [Required]
        public virtual ICollection< ViaPoints> ViaPoints { get; set; }      
    }
}
