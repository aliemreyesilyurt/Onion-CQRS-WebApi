using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Application.Exceptions;

namespace WebApi.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddTransient<ExceptionMiddleware>();
            services.AddMediatR(config => config.RegisterServicesFromAssembly(assembly));
        }
    }
}
