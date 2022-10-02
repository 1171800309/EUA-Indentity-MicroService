using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EUA.Core.Bus
{
    public interface IMediatRHandler
    {
        Task SendCommand<T>(T command) where T : Command.Command;

        Task RegisterEvent<T>(T @event) where T :Event.Event;
    }
}
