namespace EventAgency.Core.Exceptions
{
    /// <summary></summary>
    public class EventNotFoundException : EventAgencyException
    {
        public EventNotFoundException(string eventId)
            : base($"Мероприятие с id \"{eventId}\" не найдено.") { }
    }
}
