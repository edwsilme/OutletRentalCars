using Microsoft.EntityFrameworkCore;
using OutletRental.Application.Interfaces;
using OutletRental.Domain.Entities;
using OutletRental.Domain.ValueObjects;
using OutletRental.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Infrastructure.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public BookingRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task AddAsync(Booking booking)
        {
            _applicationDbContext.Bookings.Add(booking);

            await _applicationDbContext.SaveChangesAsync();
        }

        public async Task<bool> HasOverlappingBookingAsync(int vehicleId, DateTime pickupDate, DateTime returnDate)
        {
            var bookings = await _applicationDbContext.Bookings
            .Where(b => b.VehicleId == vehicleId)
            .ToListAsync();

            var range = new DateRange(pickupDate, returnDate);

            return bookings.Any(b => b.Overlaps(range));
        }
    }
}
