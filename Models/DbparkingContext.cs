using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EstacionamientoAPI.Models;

public partial class DbparkingContext : DbContext
{
    public DbparkingContext()
    {
    }

    public DbparkingContext(DbContextOptions<DbparkingContext> options)
        : base(options)
    {
    }

    public virtual DbSet<ParkingRecord> ParkingRecords { get; set; }

    public virtual DbSet<VehicleRegister> VehicleRegisters { get; set; }

    public virtual DbSet<VehicleType> VehicleTypes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ParkingRecord>(entity =>
        {
            entity.HasKey(e => e.IdParkingRecord);

            entity.ToTable("PARKING_RECORD");

            entity.Property(e => e.IdParkingRecord)
                .ValueGeneratedOnAdd()
                .HasColumnName("idParkingRecord");
            entity.Property(e => e.PlateNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("plateNumber");
            entity.Property(e => e.ArrivalTime)
                .HasPrecision(0)
                .HasColumnName("arrivalTime");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.DepartureTime)
                .HasPrecision(0)
                .HasColumnName("departureTime");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.ParkedTime).HasColumnName("parkedTime");
            entity.Property(e => e.Payment)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("payment");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            /*entity.HasOne(d => d.PlateNumberNavigation).WithMany(p => p.ParkingRecords)
                .HasForeignKey(d => d.PlateNumber)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PARKING_RECORD_VEHICLE_REGISTER");*/
        });

        modelBuilder.Entity<VehicleRegister>(entity =>
        {
            entity.HasKey(e => e.PlateNumber);

            entity.ToTable("VEHICLE_REGISTER");

            entity.Property(e => e.PlateNumber)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("plateNumber");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.IdVehicleType).HasColumnName("idVehicleType");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");

            /*entity.HasOne(d => d.IdVehicleTypeNavigation).WithMany(p => p.VehicleRegisters)
                .HasForeignKey(d => d.IdVehicleType)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_VEHICLE_REGISTER_VEHICLE_TYPE");*/
        });

        modelBuilder.Entity<VehicleType>(entity =>
        {
            entity.HasKey(e => e.IdVehicleType);

            entity.ToTable("VEHICLE_TYPE");

            entity.Property(e => e.IdVehicleType).HasColumnName("idVehicleType");
            entity.Property(e => e.CreationDate)
                .HasColumnType("datetime")
                .HasColumnName("creationDate");
            entity.Property(e => e.NameType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("nameType");
            entity.Property(e => e.RatePerMinute)
                .HasColumnType("decimal(5, 2)")
                .HasColumnName("ratePerMinute");
            entity.Property(e => e.UpdateDate)
                .HasColumnType("datetime")
                .HasColumnName("updateDate");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
