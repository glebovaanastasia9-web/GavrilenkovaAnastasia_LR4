namespace EventAgency.Core.Exceptions
{
    /// <summary></summary>
    public class NotEnoughSeatsException : EventAgencyException
    {
        public NotEnoughSeatsException(string eventTitle, int available, int requested)
            : base($"Недостаточно мест на мероприятии \"{eventTitle}\": доступно {available}, запрошено {requested}.") { }
    }
}
