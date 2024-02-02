﻿// <auto-generated />
using System;
using EstacionamientoAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EstacionamientoAPI.Migrations
{
    [DbContext(typeof(DbparkingContext))]
    [Migration("20240202181812_MigrationParking")]
    partial class MigrationParking
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EstacionamientoAPI.Models.ParkingRecord", b =>
                {
                    b.Property<int>("IdParkingRecord")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idParkingRecord");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdParkingRecord"));

                    b.Property<TimeOnly?>("ArrivalTime")
                        .HasPrecision(0)
                        .HasColumnType("time(0)")
                        .HasColumnName("arrivalTime");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime")
                        .HasColumnName("creationDate");

                    b.Property<TimeOnly?>("DepartureTime")
                        .HasPrecision(0)
                        .HasColumnType("time(0)")
                        .HasColumnName("departureTime");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit")
                        .HasColumnName("isActive");

                    b.Property<int?>("ParkedTime")
                        .HasColumnType("int")
                        .HasColumnName("parkedTime");

                    b.Property<decimal?>("Payment")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("payment");

                    b.Property<string>("PlateNumber")
                        .IsRequired()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("plateNumber");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updateDate");

                    b.HasKey("IdParkingRecord");

                    b.HasIndex("PlateNumber");

                    b.ToTable("PARKING_RECORD", (string)null);
                });

            modelBuilder.Entity("EstacionamientoAPI.Models.VehicleRegister", b =>
                {
                    b.Property<string>("PlateNumber")
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("plateNumber");

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime")
                        .HasColumnName("creationDate");

                    b.Property<int>("IdVehicleType")
                        .HasColumnType("int")
                        .HasColumnName("idVehicleType");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updateDate");

                    b.HasKey("PlateNumber");

                    b.HasIndex("IdVehicleType");

                    b.ToTable("VEHICLE_REGISTER", (string)null);
                });

            modelBuilder.Entity("EstacionamientoAPI.Models.VehicleType", b =>
                {
                    b.Property<int>("IdVehicleType")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("idVehicleType");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("IdVehicleType"));

                    b.Property<DateTime>("CreationDate")
                        .HasColumnType("datetime")
                        .HasColumnName("creationDate");

                    b.Property<string>("NameType")
                        .HasMaxLength(20)
                        .IsUnicode(false)
                        .HasColumnType("varchar(20)")
                        .HasColumnName("nameType");

                    b.Property<decimal?>("RatePerMinute")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("ratePerMinute");

                    b.Property<DateTime?>("UpdateDate")
                        .HasColumnType("datetime")
                        .HasColumnName("updateDate");

                    b.HasKey("IdVehicleType");

                    b.ToTable("VEHICLE_TYPE", (string)null);
                });

            modelBuilder.Entity("EstacionamientoAPI.Models.ParkingRecord", b =>
                {
                    b.HasOne("EstacionamientoAPI.Models.VehicleRegister", "PlateNumberNavigation")
                        .WithMany("ParkingRecords")
                        .HasForeignKey("PlateNumber")
                        .IsRequired()
                        .HasConstraintName("FK_PARKING_RECORD_VEHICLE_REGISTER");

                    b.Navigation("PlateNumberNavigation");
                });

            modelBuilder.Entity("EstacionamientoAPI.Models.VehicleRegister", b =>
                {
                    b.HasOne("EstacionamientoAPI.Models.VehicleType", "IdVehicleTypeNavigation")
                        .WithMany("VehicleRegisters")
                        .HasForeignKey("IdVehicleType")
                        .IsRequired()
                        .HasConstraintName("FK_VEHICLE_REGISTER_VEHICLE_TYPE");

                    b.Navigation("IdVehicleTypeNavigation");
                });

            modelBuilder.Entity("EstacionamientoAPI.Models.VehicleRegister", b =>
                {
                    b.Navigation("ParkingRecords");
                });

            modelBuilder.Entity("EstacionamientoAPI.Models.VehicleType", b =>
                {
                    b.Navigation("VehicleRegisters");
                });
#pragma warning restore 612, 618
        }
    }
}
