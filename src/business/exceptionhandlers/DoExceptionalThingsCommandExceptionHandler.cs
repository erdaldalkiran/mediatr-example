using System;
using System.Threading;
using System.Threading.Tasks;
using business.commands;
using MediatR;
using MediatR.Pipeline;

namespace business.exceptionhandlers
{
    public class DoExceptionalThingsCommandExceptionHandler : IRequestExceptionHandler<DoExceptionalThingsCommandAsync, Unit, ArgumentException>
    {
        public Task Handle(
            DoExceptionalThingsCommandAsync request,
            ArgumentException exception,
            RequestExceptionHandlerState<Unit> state,
            CancellationToken cancellationToken)
        {
            state.SetHandled(Unit.Value);
            Console.WriteLine("there is nothing to see here! just swallowing an exception!");
            return Task.CompletedTask;
        }

    }
}