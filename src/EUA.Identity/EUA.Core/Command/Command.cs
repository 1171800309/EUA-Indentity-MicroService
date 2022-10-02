using FluentValidation;
using FluentValidation.Results;
using MediatR;
using System;

namespace EUA.Core.Command
{
    public abstract class Command : IRequest
    {
        public DateTime Timestamp { get; private set; }

        public ValidationResult ValidateResult { get; set; }

        protected Command()
        {
            Timestamp = DateTime.Now;
        }

        public abstract bool IsValid(IValidator validator);
    }
}
