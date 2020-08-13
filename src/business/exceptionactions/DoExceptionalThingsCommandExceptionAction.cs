using System;
using System.Threading;
using System.Threading.Tasks;
using business.commands;
using MediatR.Pipeline;

namespace business.exceptionactions
{
    public class
        DoExceptionalThingsCommandExceptionAction : IRequestExceptionAction<DoExceptionalThingsCommandAsync,
            ArgumentException>
    {
        public Task Execute(DoExceptionalThingsCommandAsync request, ArgumentException exception,
            CancellationToken cancellationToken)
        {
            Console.WriteLine("perfoming an action on exception");
            return Task.CompletedTask;
        }
    }
}