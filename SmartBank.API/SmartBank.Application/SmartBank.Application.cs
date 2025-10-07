using Mapster;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SmartBank.Application.Persistence;
using System.Reflection;

namespace SmartBank.Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationService(this IServiceCollection services)
        {
            services.AddMapster();
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }

    }
}
