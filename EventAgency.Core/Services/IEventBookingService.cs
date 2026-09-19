using System.Collections.Generic;
using EventAgency.Core.Models;

namespace EventAgency.Core.Services
{
    /// <summary> </summary>
    public interface IEventBookingService
    {
        void RegisterEvent(AgencyEvent newEvent);
        AgencyEvent GetEventById(string eventId);
        List<AgencyEvent> GetAllEvents();

        Booking BookSeats(string eventId, string clientName, int guestCount);
        void CancelBooking(string bookingId);
        List<Booking> GetBookingsForEvent(string eventId);
        int GetAvailableSeats(string eventId);
    }
}
