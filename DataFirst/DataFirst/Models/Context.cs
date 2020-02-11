using System;
using System.Collections.Generic;
using CarPoolApplication.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CodeFirst.Models
{
    public partial class Context : DbContext
    {


        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Offer> Offers { get; set; }
        public DbSet<Rider> Riders { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<ViaPoints> ViaPoints { get; set; }

        public Context(DbContextOptions <Context> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\aman.j\\Documents\\Sample.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<ViaPoints>().Property(c => c.City).HasConversion(v => v.ToString(), v => (Cities)Enum.Parse(typeof(Cities), v));
            modelBuilder.Entity<Offer>().Property(c => c.Status).HasConversion(v => v.ToString(), v => (StatusOfRide)Enum.Parse(typeof(StatusOfRide), v));
            modelBuilder.Entity<Booking>().Property(c => c.Status).HasConversion(v => v.ToString(), v => (StatusOfRide)Enum.Parse(typeof(StatusOfRide), v));
            modelBuilder.Entity<Vehicle>().Property(c => c.Type).HasConversion(v => v.ToString(), v => (VehicleType)Enum.Parse(typeof(VehicleType), v));
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
