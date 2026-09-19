using System;

namespace EventAgency.Core.Models
{
    /// <summary></summary>
    public class AgencyEvent
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DateTime EventDate { get; set; }

        /// <summary></summary>
        public int Capacity { get; set; }

        public bool IsCancelled { get; set; }

        public AgencyEvent(string id, string title, DateTime eventDate, int capacity)
        {
            Id = id;
            Title = title;
            EventDate = eventDate;
            Capacity = capacity;
            IsCancelled = false;
        }

        public override string ToString()
        {
            return $"{Title} ({EventDate:dd.MM.yyyy}), вместимость {Capacity}";
        }
    }
}
