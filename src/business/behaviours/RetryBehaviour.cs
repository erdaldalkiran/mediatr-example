using System;
using System.Threading;
using System.Threading.Tasks;
using business.commands;
using MediatR;
using Polly;
using Polly.Retry;

namespace business.behaviours
{
    public class RetryBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private AsyncRetryPolicy policy;

        public RetryBehaviour(IRetrier<TRequest> retrier = null)
        {
            if (retrier != null)
            {
                policy = retrier.GetRetryPolicy();
            }
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            if (policy == null)
            {
                return await next();
            }

            var counter = 0;
            var result = await policy.ExecuteAsync(async () =>
            {
                Console.WriteLine(counter);
                
                counter++;
                return await next();
            });
            return result;
        }
    }

    public interface IRetriable { }

    public interface IRetrier<TRequest> {
        AsyncRetryPolicy GetRetryPolicy();
    }

    public class GenericRetrier<TRequest> : IRetrier<TRequest> where TRequest : IRetriable
    {
        public AsyncRetryPolicy GetRetryPolicy()
        {
            return Policy.Handle<Exception>().WaitAndRetryAsync(5, i =>
            {
                Console.WriteLine($"Generic {i}");
                return TimeSpan.FromMilliseconds(200 * i);
            });
        }
    }

    public class DoExceptionalThingsRetrier : IRetrier<DoExceptionalThingsCommandAsync> 
    {
        public AsyncRetryPolicy GetRetryPolicy()
        {
            return Policy.Handle<ArgumentException>().WaitAndRetryAsync(5, i =>
            {
                Console.WriteLine($"Specific {i}");
                return TimeSpan.FromMilliseconds(200 * i);
            });
        }
    }


}
