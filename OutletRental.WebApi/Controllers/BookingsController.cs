using Microsoft.AspNetCore.Mvc;
using OutletRental.Application.Features.Bookings.Create;
using System.Reflection.Metadata;

namespace OutletRental.WebApi.Controllers
{
    [ApiController]
    [Route("api/bookings")]
    public class BookingsController : ControllerBase
    {
        private readonly CreateBookingHandler _createHandler;

        public BookingsController(CreateBookingHandler createHandler) 
        { 
            _createHandler = createHandler; 
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateBookingCommand command)
        {
            try
            {
                var bookingId = await _createHandler.Handle(command);

                return Ok(new
                {
                    Message = "¡Reserva creada exitosamente!",
                    BookingId = bookingId,
                    Status = "Confirmada"
                });
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new
                {
                    Error = "Disponibilidad",
                    Details = ex.Message
                });
            }
            catch (Exception)
            {
                return StatusCode(500, "Ocurrió un error interno al procesar su reserva.");
            }
        }
    }
}
