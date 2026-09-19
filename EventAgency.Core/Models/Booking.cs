using System;

namespace EventAgency.Core.Models
{
    /// <summary></summary>
    public class Booking
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public string ClientName { get; set; }
        public int GuestCount { get; set; }
        public DateTime BookingDate { get; set; }
        public bool IsCancelled { get; set; }

        public Booking(string id, string eventId, string clientName, int guestCount, DateTime bookingDate)
        {
            Id = id;
            EventId = eventId;
            ClientName = clientName;
            GuestCount = guestCount;
            BookingDate = bookingDate;
            IsCancelled = false;
        }

        public override string ToString()
        {
            return $"Бронь {Id}: {ClientName}, гостей {GuestCount}";
        }
    }
}
