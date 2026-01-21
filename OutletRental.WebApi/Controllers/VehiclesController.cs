using Microsoft.AspNetCore.Mvc;
using OutletRental.Application.Features.Vehicles.Search;
using System.Reflection.Metadata;

namespace OutletRental.WebApi.Controllers
{
    [ApiController]
    [Route("api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly SearchVehiclesHandler _searchHandler;

        public VehiclesController(SearchVehiclesHandler searchHandler)
        {
            _searchHandler = searchHandler;
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search(int pickupLocationId, DateTime pickupDate, DateTime returnDate)
        {
            try
            {
                if (pickupLocationId <= 0)
                {
                    return BadRequest("La localidad de recogida es obligatoria.");
                }

                if (returnDate <= pickupDate)
                {
                    return BadRequest("La fecha de devolución debe ser posterior a la de recogida.");
                }

                var query = new SearchVehiclesQuery(
                    pickupLocationId,
                    pickupDate,
                    returnDate);

                var vehicles = await _searchHandler.Handle(query);

                return Ok(vehicles);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Message = "Ocurrió un error inesperado al buscar vehículos.",
                    Details = ex.Message
                });
            }
        }
    }
}
