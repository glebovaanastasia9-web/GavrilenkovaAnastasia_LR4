namespace EventAgency.Core.Exceptions
{
    /// <summary></summary>
    public class InvalidBookingDataException : EventAgencyException
    {
        public InvalidBookingDataException(string message) : base(message) { }
    }
}
