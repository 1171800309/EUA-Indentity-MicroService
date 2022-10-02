using MediatR;
using System;

namespace EUA.Core.Event
{
    public abstract class Event : INotification
    {
        public DateTime Timestamp { get; private set; }

        protected Event()
        {
            this.Timestamp = DateTime.Now;
        }
    }
}
