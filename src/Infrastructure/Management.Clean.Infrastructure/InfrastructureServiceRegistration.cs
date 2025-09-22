using Management.Clean.Application.Contracts.Email;
using Management.Clean.Application.Contracts.Logging;
using Management.Clean.Application.Models.Emails;
using Management.Clean.Infrastructure.Constants;
using Management.Clean.Infrastructure.EmailService;
using Management.Clean.Infrastructure.Logging;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Management.Clean.Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<EmailSettings>(configuration.GetSection(Settings.EmailSettings));
        services.AddTransient<IEmailSender, EmailSender>();
        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

        return services;
    }
}