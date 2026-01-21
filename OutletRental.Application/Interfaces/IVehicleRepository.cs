using OutletRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Application.Interfaces
{
    public interface IVehicleRepository
    {
        Task<Vehicle?> GetByIdAsync(int vehicleId);
        Task<List<Vehicle>> GetAvailableVehiclesAsync(int pickupLocationId, DateTime pickupDate, DateTime returnDate);
    }
}
