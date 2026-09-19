namespace EventAgency.Core.Exceptions
{
    /// <summary></summary>
    public class BookingNotFoundException : EventAgencyException
    {
        public BookingNotFoundException(string bookingId)
            : base($"Бронирование с id \"{bookingId}\" не найдено.") { }
    }
}
