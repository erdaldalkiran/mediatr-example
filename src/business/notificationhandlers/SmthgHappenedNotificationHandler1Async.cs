using System;
using System.Threading;
using System.Threading.Tasks;
using business.notifications;
using MediatR;

namespace business.notificationhandlers
{
    public class SmthgHappenedNotificationHandler1Async : INotificationHandler<SmthgHappenedNotification>
    {
        private int counter;

        public async Task Handle(SmthgHappenedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Delay(100, cancellationToken);

            Console.WriteLine($"notification 1: {counter}");

            counter++;
        }
    }
}