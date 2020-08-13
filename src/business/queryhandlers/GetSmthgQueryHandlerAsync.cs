using System;
using System.Threading;
using System.Threading.Tasks;
using business.queries;
using MediatR;

namespace business.queryhandlers
{
    public class GetSmthgQueryHandlerAsync : IRequestHandler<GetSmthgQueryAsync, string>
    {
        private int counter;

        public async Task<string> Handle(GetSmthgQueryAsync request, CancellationToken cancellationToken)
        {
            await Task.Delay(TimeSpan.FromMilliseconds(100), cancellationToken);

            counter++;

            return $"returning smthg {counter}";
        }
    }
}