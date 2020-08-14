using System;
using System.Threading;
using System.Threading.Tasks;
using business.commands;
using business.notifications;
using business.queries;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace console_app.hosted_services
{
    public class Looper : BackgroundService
    {
        private readonly IServiceProvider serviceProvider;

        public Looper(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (true)
            {
                using var scope = serviceProvider.CreateScope();
                var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
                await mediator.Send(new DoSmthgCommandAsync(), stoppingToken);


                var res = await mediator.Send(new GetSmthgQueryAsync(), stoppingToken);
                Console.WriteLine(res);


                await mediator.Publish(new SmthgHappenedNotification(), stoppingToken);


                await mediator.Send(new DoExceptionalThingsCommandAsync(), stoppingToken);

                await Task.Delay(TimeSpan.FromMilliseconds(100), stoppingToken);
                if (stoppingToken.IsCancellationRequested) break;
            }
        }
    }
}