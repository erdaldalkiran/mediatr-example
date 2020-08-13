using System;
using System.Threading;
using System.Threading.Tasks;
using business.notifications;
using MediatR;

namespace business.notificationhandlers
{
    public class SmthgHappenedNotificationHandler2Async : INotificationHandler<SmthgHappenedNotification>
    {
        private int counter;

        public async Task Handle(SmthgHappenedNotification notification, CancellationToken cancellationToken)
        {
            await Task.Delay(100, cancellationToken);

            Console.WriteLine($"notification 2: {counter}");

            counter++;
        }
    }
}