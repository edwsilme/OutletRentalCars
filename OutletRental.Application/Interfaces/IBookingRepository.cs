using OutletRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Application.Interfaces
{
    public interface IBookingRepository
    {
        Task AddAsync(Booking booking);
        Task<bool> HasOverlappingBookingAsync(int vehicleId, DateTime pickupDate, DateTime returnDate);
    }
}
