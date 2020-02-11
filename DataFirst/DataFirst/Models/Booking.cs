﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarPoolApplication.Models
{
    public class Booking
    {
        [Key]
        public int ID { get; set; }
        [Column(TypeName = "nvarchar(10)")]
        public StatusOfRide Status { get; set; }
        public int Source { get; set; }
        public int Destination { get; set; }
        public int OfferID { get; set; }
        public int RiderID { get; set; }
        public virtual Rider Rider { get; set; }
        public virtual Offer Offer { get; set; }
        public float Fare { get; set; }
        public byte Seats { get; set; }
    }
}
