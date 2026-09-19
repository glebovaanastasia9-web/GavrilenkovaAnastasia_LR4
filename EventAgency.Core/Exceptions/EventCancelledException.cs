namespace EventAgency.Core.Exceptions
{
    /// <summary></summary>
    public class EventCancelledException : EventAgencyException
    {
        public EventCancelledException(string eventTitle)
            : base($"Мероприятие \"{eventTitle}\" отменено, бронирование невозможно.") { }
    }
}
