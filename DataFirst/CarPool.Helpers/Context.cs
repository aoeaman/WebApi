using System;
using CarPool.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace CarPool.Helpers
{
    public partial class Context : DbContext
    {
        public DbSet<BookingDBO> Bookings { get; set; }
        public DbSet<OfferDBO> Offers { get; set; }
        public DbSet<UserDBO> Users { get; set; }
        public DbSet<VehicleDBO> Vehicles { get; set; }
        public DbSet<ViaPointsDBO> ViaPoints { get; set; }

        public Context(DbContextOptions <Context> options)
            : base(options)
        {
        }

        public Context()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\aman.j\\Documents\\CARPOOL.mdf;Integrated Security=True;Connect Timeout=30");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
            modelBuilder.Entity<ViaPointsDBO>().Property(c => c.City).HasConversion(v => v.ToString(), v => (Cities)Enum.Parse(typeof(Cities), v));
            modelBuilder.Entity<OfferDBO>().Property(c => c.Status).HasConversion(v => v.ToString(), v => (StatusOfRide)Enum.Parse(typeof(StatusOfRide), v));
            modelBuilder.Entity<BookingDBO>().Property(c => c.Status).HasConversion(v => v.ToString(), v => (StatusOfRide)Enum.Parse(typeof(StatusOfRide), v));

            modelBuilder.Entity<OfferDBO>().Property(c => c.Source).HasConversion(v => v.ToString(), v => (Cities)Enum.Parse(typeof(Cities), v));
            modelBuilder.Entity<OfferDBO>().Property(c => c.Destination).HasConversion(v => v.ToString(), v => (Cities)Enum.Parse(typeof(Cities), v));
            modelBuilder.Entity<OfferDBO>().Property(c => c.CurrentLocaton).HasConversion(v => v.ToString(), v => (Cities)Enum.Parse(typeof(Cities), v));
            modelBuilder.Entity<BookingDBO>().Property(c => c.Source).HasConversion(v => v.ToString(), v => (Cities)Enum.Parse(typeof(Cities), v));
            modelBuilder.Entity<BookingDBO>().Property(c => c.Destination).HasConversion(v => v.ToString(), v => (Cities)Enum.Parse(typeof(Cities), v));

            modelBuilder.Entity<VehicleDBO>().Property(c => c.Type).HasConversion(v => v.ToString(), v => (VehicleType)Enum.Parse(typeof(VehicleType), v));
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
