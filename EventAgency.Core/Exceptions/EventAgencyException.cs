using System;

namespace EventAgency.Core.Exceptions
{
    /// <summary> </summary>
    public abstract class EventAgencyException : Exception
    {
        protected EventAgencyException(string message) : base(message) { }
        protected EventAgencyException(string message, Exception inner) : base(message, inner) { }
    }
}
