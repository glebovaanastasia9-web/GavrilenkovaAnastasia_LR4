namespace EventAgency.Core.Exceptions
{
    /// <summary></summary>
    public class BookingAlreadyCancelledException : EventAgencyException
    {
        public BookingAlreadyCancelledException(string bookingId)
            : base($"Бронирование \"{bookingId}\" уже было отменено ранее.") { }
    }
}
