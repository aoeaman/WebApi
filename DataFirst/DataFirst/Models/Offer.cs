using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CarPoolApplication.Models
{
    public class Offer
    {
        [Key]
        public int ID { get; set; }
        public StatusOfRide Status { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
        public int CurrentLocaton { get; set; }       
        public virtual Driver Driver { get; set; }
        public virtual Vehicle Vehicle { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public byte SeatsAvailable { get; set; }
        public float Earnings { get; set; }
        public virtual ICollection< ViaPoints> ViaPoints { get; set; }      
    }
}
