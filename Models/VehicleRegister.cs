using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EstacionamientoAPI.Models;

public partial class VehicleRegister
{
    public string PlateNumber { get; set; } = null!;

    public int IdVehicleType { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    /*[JsonIgnore]
    public virtual VehicleType IdVehicleTypeNavigation { get; set; }

    public virtual ICollection<ParkingRecord> ParkingRecords { get; set; } = new List<ParkingRecord>();*/
}
