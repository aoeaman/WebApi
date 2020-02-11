using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CodeFirst.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Drivers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    Age = table.Column<byte>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    DrivingLiscenceNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Drivers", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Riders",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    Age = table.Column<byte>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Riders", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverID = table.Column<int>(nullable: false),
                    VehicleID = table.Column<int>(nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    CurrentLocaton = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    SeatsAvailable = table.Column<byte>(nullable: false),
                    Earnings = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Offers_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverID = table.Column<int>(nullable: false),
                    Maker = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Seats = table.Column<byte>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vehicles_Drivers_DriverID",
                        column: x => x.DriverID,
                        principalTable: "Drivers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    OfferID = table.Column<int>(nullable: false),
                    RiderID = table.Column<int>(nullable: false),
                    Fare = table.Column<float>(nullable: false),
                    Seats = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bookings_Offers_OfferID",
                        column: x => x.OfferID,
                        principalTable: "Offers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Bookings_Riders_RiderID",
                        column: x => x.RiderID,
                        principalTable: "Riders",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ViaPoints",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OfferID = table.Column<int>(nullable: false),
                    City = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViaPoints", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ViaPoints_Offers_OfferID",
                        column: x => x.OfferID,
                        principalTable: "Offers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_OfferID",
                table: "Bookings",
                column: "OfferID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RiderID",
                table: "Bookings",
                column: "RiderID");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_DriverID",
                table: "Offers",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_DriverID",
                table: "Vehicles",
                column: "DriverID");

            migrationBuilder.CreateIndex(
                name: "IX_ViaPoints_OfferID",
                table: "ViaPoints",
                column: "OfferID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "ViaPoints");

            migrationBuilder.DropTable(
                name: "Riders");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Drivers");
        }
    }
}
