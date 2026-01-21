using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Application.Features.Bookings.Create
{
    public class CreateBookingCommand
    {
        public int VehicleId { get; }
        public int PickupLocationId { get; }
        public int ReturnLocationId { get; }
        public DateTime PickupDate { get; }
        public DateTime ReturnDate { get; }

        public CreateBookingCommand(int vehicleId,  int pickupLocationId, int returnLocationId, DateTime pickupDate, DateTime returnDate)
        {
            VehicleId = vehicleId;
            PickupLocationId = pickupLocationId;
            ReturnLocationId = returnLocationId;
            PickupDate = pickupDate;
            ReturnDate = returnDate;
        }
    }
}
