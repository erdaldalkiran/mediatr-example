using System;
using System.Threading;
using System.Threading.Tasks;
using business.commands;
using MediatR;

namespace business.commandhandlers
{
    public class DoExceptionalThingsCommandHandlerAsync : AsyncRequestHandler<DoExceptionalThingsCommandAsync>
    {
        protected override Task Handle(DoExceptionalThingsCommandAsync request, CancellationToken cancellationToken)
        {
            Console.WriteLine("doing exceptional things...");
            throw new ArgumentException("ooppsy");
        }
    }
}