﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace WebApi.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        }
    }
}
