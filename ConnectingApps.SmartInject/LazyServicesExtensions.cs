﻿using Microsoft.Extensions.DependencyInjection;
using System;

namespace ConnectingApps.SmartInject
{
    public static class LazyServicesExtensions
    {
        public static IServiceCollection AddLazyTransient<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            return services.AddTransient<TService, TImplementation>()
                .AddTransient(a => new Lazy<TService>(a.GetRequiredService<TService>));
        }
    }
}
