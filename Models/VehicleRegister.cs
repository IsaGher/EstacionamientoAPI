using System;
using System.Collections.Generic;

namespace EstacionamientoAPI.Models;

public partial class VehicleRegister
{
    public string PlateNumber { get; set; } = null!;

    public int IdVehicleType { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual VehicleType IdVehicleTypeNavigation { get; set; } = null!;

    public virtual ICollection<ParkingRecord> ParkingRecords { get; set; } = new List<ParkingRecord>();
}
