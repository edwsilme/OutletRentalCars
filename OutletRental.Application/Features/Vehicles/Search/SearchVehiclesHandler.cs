using OutletRental.Application.Interfaces;
using OutletRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Application.Features.Vehicles.Search
{
    public class SearchVehiclesHandler
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMarketRepository _marketRepository;

        public SearchVehiclesHandler(IVehicleRepository vehicleRepository, IMarketRepository marketRepository)
        {
            _vehicleRepository = vehicleRepository;
            _marketRepository = marketRepository;
        }

        public async Task<List<Vehicle>> Handle(SearchVehiclesQuery query)
        {
            var availableInSql = await _vehicleRepository.GetAvailableVehiclesAsync(
                    query.PickupLocationId,
                    query.PickupDate,
                    query.ReturnDate);

            var marketConfig = await _marketRepository.GetMarketByCountryAsync("CO");

            return availableInSql
            .Where(v => marketConfig.AllowedVehicleCategories.Contains(v.Category))
            .ToList();
        }
    }
}
