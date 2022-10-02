using AutoMapper;
using EUA.Core.Bus;

namespace EUA.Core.Event
{
    public class EventHandler
    {
        protected readonly IMediatRHandler _bus;
        protected readonly IMapper _map;

        public EventHandler(IMediatRHandler bus, IMapper map)
        {
            this._bus = bus;
            this._map = map;
        }
    }
}
