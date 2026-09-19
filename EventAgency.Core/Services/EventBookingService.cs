using System;
using System.Collections.Generic;
using System.Linq;
using EventAgency.Core.Exceptions;
using EventAgency.Core.Models;

namespace EventAgency.Core.Services
{
    /// <summary> </summary>
    public class EventBookingService : IEventBookingService
    {
        private readonly Dictionary<string, AgencyEvent> events = new Dictionary<string, AgencyEvent>();
        private readonly Dictionary<string, Booking> bookings = new Dictionary<string, Booking>();

        public void RegisterEvent(AgencyEvent newEvent)
        {
            if (newEvent == null)
                throw new ArgumentNullException(nameof(newEvent));

            if (string.IsNullOrWhiteSpace(newEvent.Id))
                throw new InvalidEventDataException("Id мероприятия не может быть пустым.");

            if (string.IsNullOrWhiteSpace(newEvent.Title))
                throw new InvalidEventDataException("Название мероприятия не может быть пустым.");

            if (newEvent.Capacity <= 0)
                throw new InvalidEventDataException("Вместимость мероприятия должна быть больше нуля.");

            if (events.ContainsKey(newEvent.Id))
                throw new InvalidEventDataException($"Мероприятие с id \"{newEvent.Id}\" уже зарегистрировано.");

            events[newEvent.Id] = newEvent;
        }

        public AgencyEvent GetEventById(string eventId)
        {
            if (!events.TryGetValue(eventId, out AgencyEvent found))
                throw new EventNotFoundException(eventId);

            return found;
        }

        public List<AgencyEvent> GetAllEvents()
        {
            return events.Values.ToList();
        }

        public Booking BookSeats(string eventId, string clientName, int guestCount)
        {
            AgencyEvent agencyEvent = GetEventById(eventId); // бросит EventNotFoundException, если события нет

            if (agencyEvent.IsCancelled)
                throw new EventCancelledException(agencyEvent.Title);

            if (string.IsNullOrWhiteSpace(clientName))
                throw new InvalidBookingDataException("Имя клиента не может быть пустым.");

            if (guestCount <= 0)
                throw new InvalidBookingDataException("Количество гостей должно быть больше нуля.");

            int available = GetAvailableSeats(eventId);
            if (guestCount > available)
                throw new NotEnoughSeatsException(agencyEvent.Title, available, guestCount);

            var booking = new Booking(Guid.NewGuid().ToString(), eventId, clientName, guestCount, DateTime.Now);
            bookings[booking.Id] = booking;
            return booking;
        }

        public void CancelBooking(string bookingId)
        {
            if (!bookings.TryGetValue(bookingId, out Booking booking))
                throw new BookingNotFoundException(bookingId);

            if (booking.IsCancelled)
                throw new BookingAlreadyCancelledException(bookingId);

            booking.IsCancelled = true;
        }

        public List<Booking> GetBookingsForEvent(string eventId)
        {
            GetEventById(eventId); // проверяет, что мероприятие существует

            return bookings.Values
                .Where(b => b.EventId == eventId && !b.IsCancelled)
                .ToList();
        }

        public int GetAvailableSeats(string eventId)
        {
            AgencyEvent agencyEvent = GetEventById(eventId);

            int booked = bookings.Values
                .Where(b => b.EventId == eventId && !b.IsCancelled)
                .Sum(b => b.GuestCount);

            return agencyEvent.Capacity - booked;
        }
    }
}
