namespace EventAgency.Core.Exceptions
{
    /// <summary></summary>
    public class InvalidEventDataException : EventAgencyException
    {
        public InvalidEventDataException(string message) : base(message) { }
    }
}
