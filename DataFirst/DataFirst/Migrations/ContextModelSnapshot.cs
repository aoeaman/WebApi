﻿// <auto-generated />
using System;
using CodeFirst;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CarPoolApplication.Migrations
{
    [DbContext(typeof(Context))]
    partial class ContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("CarPool.Data.Models.BookingDBO", b =>
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

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("OfferID")
                        .HasColumnType("int");

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("OfferID");

                    b.HasIndex("UserID");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("CarPool.Data.Models.OfferDBO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CurrentLocaton")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("Destination")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<float>("Earnings")
                        .HasColumnType("real");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<int>("SeatsAvailable")
                        .HasColumnType("int");

                    b.Property<string>("Source")
                        .IsRequired()
                        .HasColumnType("nvarchar(15)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(10)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.Property<int>("VehicleID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.HasIndex("VehicleID");

                    b.ToTable("Offers");
                });

            modelBuilder.Entity("CarPool.Data.Models.UserDBO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<string>("DrivingLiscenceNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("nvarchar(1)");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Role")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Token")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ID");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("CarPool.Data.Models.VehicleDBO", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<string>("Maker")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Number")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Seats")
                        .HasColumnType("int");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserID")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("UserID");

                    b.ToTable("Vehicles");
                });

            modelBuilder.Entity("CarPool.Data.Models.ViaPointsDBO", b =>
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

            modelBuilder.Entity("CarPool.Data.Models.BookingDBO", b =>
                {
                    b.HasOne("CarPool.Data.Models.OfferDBO", "Offer")
                        .WithMany()
                        .HasForeignKey("OfferID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarPool.Data.Models.UserDBO", "User")
                        .WithMany("Bookings")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.Data.Models.OfferDBO", b =>
                {
                    b.HasOne("CarPool.Data.Models.UserDBO", "User")
                        .WithMany("Offers")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarPool.Data.Models.VehicleDBO", "Vehicle")
                        .WithMany("Offers")
                        .HasForeignKey("VehicleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.Data.Models.VehicleDBO", b =>
                {
                    b.HasOne("CarPool.Data.Models.UserDBO", "User")
                        .WithMany("Vehicles")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarPool.Data.Models.ViaPointsDBO", b =>
                {
                    b.HasOne("CarPool.Data.Models.OfferDBO", "Offer")
                        .WithMany("ViaPoints")
                        .HasForeignKey("OfferID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
