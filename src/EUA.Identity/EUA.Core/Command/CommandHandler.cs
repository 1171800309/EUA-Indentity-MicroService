using AutoMapper;
using EUA.Core.Bus;
using EUA.Core.Notification;

namespace EUA.Core.Command
{
    public abstract class CommandHandler
    {
        protected readonly IMediatRHandler _bus;
        protected readonly IMapper _map;

        public CommandHandler(IMediatRHandler bus, IMapper map)
        {
            this._bus = bus;
            this._map = map;
        }

        protected void RegisterValidateErrors(Command command)
        {
            foreach (var errors in command.ValidateResult.Errors)
                _bus.RegisterEvent(new DomainNotification(command.GetType().FullName, errors.ErrorMessage));
        }
    }
}
