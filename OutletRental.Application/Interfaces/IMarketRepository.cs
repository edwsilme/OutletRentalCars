using OutletRental.Domain.Entities;

namespace OutletRental.Application.Interfaces
{
    public interface IMarketRepository
    {
        Task<Market> GetMarketByCountryAsync(string countryCode);
    }
}