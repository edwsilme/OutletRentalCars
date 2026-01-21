using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Domain.Entities
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string Model { get; set; } = string.Empty;
        public string Plate { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public VehicleStatus Status { get; set; }
        public int CurrentLocationId { get; set; }
        public Location CurrentLocation { get; set; } = null!;
        public string CountryCode { get; set; } = string.Empty;

        public bool IsAvailable() => Status == VehicleStatus.Disponible;
    }

    public enum VehicleStatus
    {
        Disponible = 1,
        NoDisponible = 0
    }
}
