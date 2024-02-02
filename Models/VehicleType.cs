using System;
using System.Collections.Generic;

namespace EstacionamientoAPI.Models;

public partial class VehicleType
{
    public int IdVehicleType { get; set; }

    public string? NameType { get; set; }

    public decimal? RatePerMinute { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    public virtual ICollection<VehicleRegister> VehicleRegisters { get; set; } = new List<VehicleRegister>();
}
