using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Application.Features.Vehicles.Search
{
    public class SearchVehiclesQuery
    {
        public int PickupLocationId { get; }
        public DateTime PickupDate { get; }
        public DateTime ReturnDate { get; }

        public SearchVehiclesQuery(int pickupLocationId, DateTime pickupDate, DateTime returnDate)
        {
            PickupLocationId = pickupLocationId;
            PickupDate = pickupDate;
            ReturnDate = returnDate;
        }
    }
}
