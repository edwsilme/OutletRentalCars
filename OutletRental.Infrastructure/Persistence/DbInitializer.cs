using MongoDB.Driver;
using OutletRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Infrastructure.Persistence
{
    public class DbInitializer
    {
        public static async Task SeedData(ApplicationDbContext mysqlContext, IMongoDatabase mongoDb)
        {
            if (!mysqlContext.Vehicles.Any())
            {
                var bogota = new Location { Name = "Aeropuerto El Dorado", CountryCode = "CO" };
                mysqlContext.Vehicles.Add(new Vehicle
                {
                    Model = "Toyota Corolla",
                    Plate = "ABC123",
                    Status = VehicleStatus.Disponible,
                    CurrentLocation = bogota,
                    CountryCode = "CO"
                });
                await mysqlContext.SaveChangesAsync();
            }

            var marketsCollection = mongoDb.GetCollection<Market>("Markets");
            if (await marketsCollection.CountDocumentsAsync(_ => true) == 0)
            {
                await marketsCollection.InsertOneAsync(new Market
                {
                    CountryCode = "CO",
                    AllowedVehicleCategories = new List<string> { "Sedan", "SUV" }
                });
            }
        }
    }
}
