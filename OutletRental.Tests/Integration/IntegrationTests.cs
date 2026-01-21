using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Tests.Integration
{
    public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public IntegrationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Get_Search_Endpoint_Returns_Success()
        {
            // ACT
            // Hacemos una petición HTTP real a tu controlador
            var response = await _client.GetAsync("/api/vehicles/search?pickupLocationId=1&pickupDate=2026-02-20&returnDate=2026-02-25");

            // ASSERT
            // Verificamos que responda código 200 OK (incluso si la lista está vacía)
            response.EnsureSuccessStatusCode();
            var responseString = await response.Content.ReadAsStringAsync();

            Assert.False(string.IsNullOrEmpty(responseString));
        }
    }
}
