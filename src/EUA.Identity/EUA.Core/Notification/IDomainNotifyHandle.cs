using System;
using System.Collections.Generic;
using System.Text;

namespace EUA.Core.Notification
{
    public interface IDomainNotifyHandle
    {
        void SetNotify(DomainNotification message);
        List<DomainNotification> GetNotifys(string key = "");
        string GetAllNotify(string key = "");
        void Dispose();
    }
}
