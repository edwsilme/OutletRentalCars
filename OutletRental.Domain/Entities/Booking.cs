using OutletRental.Domain.Common;
using OutletRental.Domain.Events;
using OutletRental.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Domain.Entities
{
    public class Booking : Entity
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int PickupLocationId { get; set; }
        public int ReturnLocationId { get; set; }
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }

        private Booking() { }

        public Booking(int vehicleId, int pickupLocationId, int returnLocationId, DateTime pickupDate, DateTime returnDate)
        {
            VehicleId = vehicleId;
            PickupLocationId = pickupLocationId;
            ReturnLocationId = returnLocationId;
            PickupDate = pickupDate;
            ReturnDate = returnDate;

            AddDomainEvent(new VehicleReservedEvent(vehicleId, pickupDate, returnDate));
        }

        public bool Overlaps(DateRange range)
        {
            var bookingRange = new DateRange(PickupDate, ReturnDate);

            return bookingRange.Overlaps(range);
        }
    }
}
