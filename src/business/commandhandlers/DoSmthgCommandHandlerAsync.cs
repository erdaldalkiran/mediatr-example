using System;
using System.Threading;
using System.Threading.Tasks;
using business.commands;
using MediatR;

namespace business.commandhandlers
{
    public class DoSmthgCommandHandlerAsync : AsyncRequestHandler<DoSmthgCommandAsync>
    {
        private int counter;

        public DoSmthgCommandHandlerAsync()
        {
            counter = 1;
        }

        protected override async Task Handle(DoSmthgCommandAsync request, CancellationToken cancellationToken)
        {
            Console.WriteLine($"hello {counter}");
            await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);
            Console.WriteLine($"bye {counter}");
            counter++;
        }
    }
}