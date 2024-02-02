using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace EstacionamientoAPI.Models;

public partial class ParkingRecord
{
    public int IdParkingRecord { get; set; }

    public string PlateNumber { get; set; }

    public bool IsActive { get; set; }

    public TimeOnly? ArrivalTime { get; set; }

    public TimeOnly? DepartureTime { get; set; }

    public int? ParkedTime { get; set; }

    public decimal? Payment { get; set; }

    public DateTime CreationDate { get; set; }

    public DateTime? UpdateDate { get; set; }

    /*[JsonIgnore]
    public virtual VehicleRegister PlateNumberNavigation { get; set; }*/
}
