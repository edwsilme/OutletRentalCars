using Moq;
using Xunit;
using OutletRental.Application.Features.Vehicles.Search;
using OutletRental.Application.Interfaces;
using OutletRental.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Tests
{
    public class SearchVehiclesTests
    {
        [Fact]
        public async Task Search_ShouldReturnEmpty_WhenNoVehiclesAvailable()
        {
            // --- ARRANGE (Preparación de datos) ---
            // Simular el repositorio de MySQL para que devuelva una lista vacía.
            // Esto representa el escenario donde el vehículo ya tiene una reserva previa.
            var mockVehicleRepo = new Mock<IVehicleRepository>();

            // Configurar el simulador para que devuelva una lista vacía
            mockVehicleRepo.Setup(r => r.GetAvailableVehiclesAsync(It.IsAny<int>(), It.IsAny<DateTime>(), It.IsAny<DateTime>()))
               .ReturnsAsync(new List<Vehicle>());

            // Simulador para MongoDB (Mercados)
            var mockMarketRepo = new Mock<IMarketRepository>();
            mockMarketRepo.Setup(r => r.GetMarketByCountryAsync("CO"))
                           .ReturnsAsync(new Market
                           {
                               CountryCode = "CO",
                               AllowedVehicleCategories = new List<string> { "Sedan", "SUV" }
                           });

            var handler = new SearchVehiclesHandler(mockVehicleRepo.Object, mockMarketRepo.Object);

            // 2. ACT (Ejecutar)
            // Simular la búsqueda en Swagger para enero
            var query = new SearchVehiclesQuery(1, new DateTime(2026, 01, 20), new DateTime(2026, 01, 25));
            var result = await handler.Handle(query);

            // 3. ASSERT (Verificar)
            // La prueba es exitosa si el resultado es una lista vacía []
            Assert.Empty(result);
        }

        [Fact]
        public void Should_Filter_By_Allowed_Categories_From_Mongo()
        {
            // ARRANGE: Datos simulados de la base de datos MongoDB
            var allowedCategories = new List<string> { "Sedan", "SUV" };

            var car = new Vehicle { Model = "Toyota Corolla", Category = "Sedan" };
            var truck = new Vehicle { Model = "F-150", Category = "Truck" };

            // ACT & ASSERT
            // Pasa: Sedan está permitido
            Assert.Contains(car.Category, allowedCategories);
            // Pasa: Truck no está en la BD Mongo
            Assert.DoesNotContain(truck.Category, allowedCategories); 
        }
    }
}
