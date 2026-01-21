using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Domain.Events
{
    public class VehicleReservedEvent
    {
        public int VehicleId { get; }
        public DateTime PickupDate { get; }
        public DateTime ReturnDate { get; }

        public VehicleReservedEvent(int vehicleId, DateTime pickupDate, DateTime returnDate)
        {
            VehicleId = vehicleId;
            PickupDate = pickupDate;
            ReturnDate = returnDate;
        }
    }
}
