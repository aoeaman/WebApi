using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarPoolApplication.Migrations
{
    public partial class ltest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: false),
                    Password = table.Column<string>(nullable: false),
                    Gender = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    DrivingLiscenceNumber = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Role = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    Maker = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Seats = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Type = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Vehicles_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(nullable: false),
                    VehicleID = table.Column<int>(nullable: false),
                    Status = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Source = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    Destination = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    CurrentLocaton = table.Column<string>(type: "nvarchar(15)", nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    SeatsAvailable = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    Earnings = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Offers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Offers_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Offers_Vehicles_VehicleID",
                        column: x => x.VehicleID,
                        principalTable: "Vehicles",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
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
                    UserID = table.Column<int>(nullable: false),
                    Fare = table.Column<float>(nullable: false),
                    Seats = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Bookings_Offers_OfferID",
                        column: x => x.OfferID,
                        principalTable: "Offers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.NoAction);
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
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_OfferID",
                table: "Bookings",
                column: "OfferID");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_UserID",
                table: "Bookings",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_UserID",
                table: "Offers",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_VehicleID",
                table: "Offers",
                column: "VehicleID");

            migrationBuilder.CreateIndex(
                name: "IX_Vehicles_UserID",
                table: "Vehicles",
                column: "UserID");

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
                name: "ViaPoints");

            migrationBuilder.DropTable(
                name: "Offers");

            migrationBuilder.DropTable(
                name: "Vehicles");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
