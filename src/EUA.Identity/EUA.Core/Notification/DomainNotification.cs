using System;

namespace EUA.Core.Notification
{
    public class DomainNotification : Core.Event.Event
    {
        public string NotifyUid { get; private set; }

        public string Key { get; private set; }

        public string Message { get; private set; }

        public DomainNotification(string key, string message)
        {
            this.NotifyUid = Guid.NewGuid().ToString();
            this.Key = key;
            this.Message = message;
        }
    }
}
