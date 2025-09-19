using Management.Clean.Application.Contrats.Persistence;
using Management.Clean.Persistence.Constants;
using Management.Clean.Persistence.DatabaseContext;
using Management.Clean.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Clean.Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        var connectionString = configuration.GetConnectionString(ConnectionStrings.HrDatabaseConnectionString);

        services.AddDbContext<HrDatabaseContext>(options =>
            options.UseSqlServer(connectionString)
        );

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<ILeaveTypeRepository, LeaveTypeRepository>();
        services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
        services.AddScoped<ILeaveAllocationRepository, LeaveAllocationRepository>();
        
        return services;
    }
}