using OutletRental.Application.Interfaces;
using OutletRental.Domain.Entities;
using OutletRental.Domain.Events;
using OutletRental.Domain.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OutletRental.Application.Features.Bookings.Create
{
    public class CreateBookingHandler
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IVehicleRepository _vehicleRepository;

        public CreateBookingHandler(
            IBookingRepository bookingRepository,
            IVehicleRepository vehicleRepository)
        {
            _bookingRepository = bookingRepository;
            _vehicleRepository = vehicleRepository;
        }

        public async Task<int> Handle(CreateBookingCommand command)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(command.VehicleId);

            if (vehicle is null)
            {
                throw new InvalidOperationException("Vehicle not found");
            }

            if (!vehicle.IsAvailable())
            {
                throw new InvalidOperationException("Vehicle not available");
            }

            var dateRange = new DateRange(
                command.PickupDate,
                command.ReturnDate);

            var hasOverlap = await _bookingRepository.HasOverlappingBookingAsync(
                command.VehicleId,
                dateRange.Start,
                dateRange.End);

            if (hasOverlap)
            {
                throw new InvalidOperationException("El vehículo seleccionado ya cuenta con una reserva para las fechas solicitadas. Por favor, elija un horario o vehículo diferente.");
            }

            var booking = new Booking(
                command.VehicleId,
                command.PickupLocationId,
                command.ReturnLocationId,
                command.PickupDate,
                command.ReturnDate);

            await _bookingRepository.AddAsync(booking);

            foreach (var domainEvent in booking.DomainEvents)
            {
                if (domainEvent is VehicleReservedEvent reservedEvent)
                {
                    Console.WriteLine($"[EVENTO DE DOMINIO EMITIDO]: Vehículo {reservedEvent.VehicleId} reservado desde {reservedEvent.PickupDate} hasta {reservedEvent.ReturnDate}");
                }
            }

            return booking.Id;

            return booking.Id;
        }
    }
}
