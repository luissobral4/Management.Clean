using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Clean.Application;

public static class ApplicationServiceRegistation
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        return services;
    }
}