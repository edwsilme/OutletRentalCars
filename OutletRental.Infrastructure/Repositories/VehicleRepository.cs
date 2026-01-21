using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using OutletRental.Application.Interfaces;
using OutletRental.Domain.Entities;
using OutletRental.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Infrastructure.Repositories
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly IMongoDatabase _mongoDatabase;

        public VehicleRepository(ApplicationDbContext applicationDbContext, IMongoDatabase mongoDatabase)
        {
            _applicationDbContext = applicationDbContext;
            _mongoDatabase = mongoDatabase;
        }

        public async Task<List<Vehicle>> GetAvailableVehiclesAsync(int pickupLocationId, DateTime pickupDate, DateTime returnDate)
        {
            var marketsCollection = _mongoDatabase.GetCollection<Market>("Markets");

            var vehiclesAtLocation = await _applicationDbContext.Vehicles
                .Include(v => v.CurrentLocation)
                .Where(v =>
                    v.CurrentLocationId == pickupLocationId &&
                    v.Status == VehicleStatus.Disponible)
                .ToListAsync();

            var availableVehicles = vehiclesAtLocation
                .Where(v => !HasOverlappingBooking(v.Id, pickupDate, returnDate))
                .ToList();

            var activeMarkets = await marketsCollection.Find(m => true).ToListAsync();
            var activeCountryCodes = activeMarkets.Select(m => m.CountryCode).ToList();

            var vehicles = availableVehicles
                .Where(v => activeCountryCodes.Contains(v.CountryCode))
                .ToList();

            return vehicles;
        }

        private bool HasOverlappingBooking(int vehicleId, DateTime pickupDate, DateTime returnDate)
        {
            var bookings = _applicationDbContext.Bookings
                .Where(b => b.VehicleId == vehicleId)
                .ToList();

            foreach (var booking in bookings)
            {
                if (DatesOverlap(pickupDate, returnDate, booking.PickupDate, booking.ReturnDate))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool DatesOverlap(DateTime pickupDate, DateTime returnDate, DateTime existingPickup, DateTime existingReturn)
        {
            return pickupDate < existingReturn && returnDate > existingPickup;
        }

        public async Task<Vehicle?> GetByIdAsync(int vehicleId)
        {
            var vehicles = await _applicationDbContext.Vehicles
                .Include(v => v.CurrentLocation)
                .FirstOrDefaultAsync(v => v.Id == vehicleId);

            return vehicles;
        }
    }
}
