using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace business.behaviours
{
    public class GenericExceptionLoggerBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            TResponse response;
            try
            {
                response = await next();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"exception {ex.GetType().Name} occurred while handling request {request.GetType().Name}");
                throw;
            }

            return response;
        }
    }
}
