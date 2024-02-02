using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EstacionamientoAPI.Migrations
{
    /// <inheritdoc />
    public partial class MigrationParking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VEHICLE_TYPE",
                columns: table => new
                {
                    idVehicleType = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    nameType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    ratePerMinute = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    creationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    updateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEHICLE_TYPE", x => x.idVehicleType);
                });

            migrationBuilder.CreateTable(
                name: "VEHICLE_REGISTER",
                columns: table => new
                {
                    plateNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    idVehicleType = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    updateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VEHICLE_REGISTER", x => x.plateNumber);
                    table.ForeignKey(
                        name: "FK_VEHICLE_REGISTER_VEHICLE_TYPE",
                        column: x => x.idVehicleType,
                        principalTable: "VEHICLE_TYPE",
                        principalColumn: "idVehicleType");
                });

            migrationBuilder.CreateTable(
                name: "PARKING_RECORD",
                columns: table => new
                {
                    idParkingRecord = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    plateNumber = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    arrivalTime = table.Column<TimeOnly>(type: "time(0)", precision: 0, nullable: true),
                    departureTime = table.Column<TimeOnly>(type: "time(0)", precision: 0, nullable: true),
                    parkedTime = table.Column<int>(type: "int", nullable: true),
                    payment = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    creationDate = table.Column<DateTime>(type: "datetime", nullable: false),
                    updateDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PARKING_RECORD", x => x.idParkingRecord);
                    table.ForeignKey(
                        name: "FK_PARKING_RECORD_VEHICLE_REGISTER",
                        column: x => x.plateNumber,
                        principalTable: "VEHICLE_REGISTER",
                        principalColumn: "plateNumber");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PARKING_RECORD_plateNumber",
                table: "PARKING_RECORD",
                column: "plateNumber");

            migrationBuilder.CreateIndex(
                name: "IX_VEHICLE_REGISTER_idVehicleType",
                table: "VEHICLE_REGISTER",
                column: "idVehicleType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PARKING_RECORD");

            migrationBuilder.DropTable(
                name: "VEHICLE_REGISTER");

            migrationBuilder.DropTable(
                name: "VEHICLE_TYPE");
        }
    }
}
