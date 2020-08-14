using System;
using System.IO;
using business.behaviours;
using business.commandhandlers;
using business.commands;
using business.exceptionactions;
using business.exceptionhandlers;
using business.notificationhandlers;
using business.notifications;
using business.queries;
using business.queryhandlers;
using console_app.hosted_services;
using MediatR;
using MediatR.Pipeline;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace console_app.config
{
    internal class Bootstrapper
    {
        public void ConfigureHost(IConfigurationBuilder cb)
        {
            cb.SetBasePath(Directory.GetCurrentDirectory());
            cb.AddJsonFile("appsettings.json", false);
            cb.AddEnvironmentVariables();
        }

        public void ConfigureServices(HostBuilderContext hbc, IServiceCollection services)
        {
            services.AddScoped<IMediator, Mediator>();
            services.AddTransient<ServiceFactory>(provider => provider.GetRequiredService);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestExceptionActionProcessorBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(GenericExceptionLoggerBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RetryBehaviour<,>));


            services.AddTransient<IRequestHandler<DoSmthgCommandAsync, Unit>, DoSmthgCommandHandlerAsync>();
            services.AddTransient<IRequestHandler<GetSmthgQueryAsync, string>, GetSmthgQueryHandlerAsync>();
            services.AddTransient<INotificationHandler<SmthgHappenedNotification>, SmthgHappenedNotificationHandler1Async>();
            services.AddTransient<INotificationHandler<SmthgHappenedNotification>, SmthgHappenedNotificationHandler2Async>();
            services.AddTransient<IRequestHandler<DoExceptionalThingsCommandAsync, Unit>, DoExceptionalThingsCommandHandlerAsync>();
            services.AddTransient<IRequestExceptionHandler<DoExceptionalThingsCommandAsync, Unit, ArgumentException>, DoExceptionalThingsCommandExceptionHandler>();
            services.AddTransient<IRequestExceptionAction<DoExceptionalThingsCommandAsync, ArgumentException>, DoExceptionalThingsCommandExceptionAction>();
            services.AddTransient<IRetrier<DoExceptionalThingsCommandAsync>, DoExceptionalThingsRetrier>();

            services.AddHostedService<Looper>();
        }
    }
}