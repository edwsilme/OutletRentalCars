using MongoDB.Driver;
using OutletRental.Application.Interfaces;
using OutletRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Infrastructure.Repositories
{
    public class MarketRepository : IMarketRepository
    {
        private readonly IMongoCollection<Market> _collection;

        public MarketRepository(IMongoClient client)
        {
            var database = client.GetDatabase("OutletRentalConfig");
            _collection = database.GetCollection<Market>("Markets");
        }

        public async Task<Market> GetMarketByCountryAsync(string countryCode)
        {
            return await _collection.Find(m => m.CountryCode == countryCode).FirstOrDefaultAsync();
        }
    }
}
