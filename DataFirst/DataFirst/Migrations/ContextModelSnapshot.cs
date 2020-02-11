﻿// <auto-generated />
using System;
using CodeFirst.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CodeFirst.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarPoolApplication.Models.Booking", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<float>("Fare")
                        .HasColumnType("real");

                    b.Property<int>("OfferID")
                        .HasColumnType("int");

                    b.Property<int>("RiderID")
                        .HasColumnType("int");

                    b.Property<byte>("Seats")
                        .HasColumnType("tinyint");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("ID");

                    b.HasIndex("OfferID");

                    b.HasIndex("RiderID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("CarPoolApplication.Models.Driver", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Age")
                        .HasColumnType("tinyint");

                    b.Property<string>("DrivingLiscenceNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Drivers");
                });

            modelBuilder.Entity("CarPoolApplication.Models.Offer", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CurrentLocaton")
                        .HasColumnType("int");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<int>("DriverID")
                        .HasColumnType("int");

                    b.Property<float>("Earnings")
                        .HasColumnType("real");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<byte>("SeatsAvailable")
                        .HasColumnType("tinyint");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("VehicleID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("DriverID");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("CarPoolApplication.Models.Rider", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<byte>("Age")
                        .HasColumnType("tinyint");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Riders");
                });

            modelBuilder.Entity("CarPoolApplication.Models.Vehicle", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("DriverID")
                        .HasColumnType("int");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Maker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte>("Seats")
                        .HasColumnType("tinyint");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.HasIndex("DriverID");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("CarPoolApplication.Models.ViaPoints", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("OfferID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OfferID");

                    b.ToTable("ViaPoints");
                });

            modelBuilder.Entity("CarPoolApplication.Models.Booking", b =>
                {
                    b.HasOne("CarPoolApplication.Models.Offer", "Offer")
                        .WithMany()
                        .HasForeignKey("OfferID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarPoolApplication.Models.Rider", "Rider")
                        .WithMany("Bookings")
                        .HasForeignKey("RiderID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPoolApplication.Models.Offer", b =>
                {
                    b.HasOne("CarPoolApplication.Models.Driver", "Driver")
                        .WithMany("Offers")
                        .HasForeignKey("DriverID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPoolApplication.Models.Vehicle", b =>
                {
                    b.HasOne("CarPoolApplication.Models.Driver", "Driver")
                        .WithMany("Vehicles")
                        .HasForeignKey("DriverID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPoolApplication.Models.ViaPoints", b =>
                {
                    b.HasOne("CarPoolApplication.Models.Offer", "Offer")
                        .WithMany("ViaPoints")
                        .HasForeignKey("OfferID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
