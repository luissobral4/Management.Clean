using System.Text;
using Management.Clean.Application.Contracts.Identity;
using Management.Clean.Application.Models.Identity;
using Management.Clean.Identity.Constants;
using Management.Clean.Identity.DbContext;
using Management.Clean.Identity.Models;
using Management.Clean.Identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Management.Clean.Identity;

public static class IdentityServicesRegistation
{
    public static IServiceCollection AddIdentityServices(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.Configure<JwtSettings>(configuration.GetSection(Configs.JwtSettings));
        var connectionString = configuration.GetConnectionString(Configs.ConnectionString);

        services.AddDbContext<HrIdentityDbContext>(options =>
            options.UseSqlServer(connectionString)
        );

        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<HrIdentityDbContext>()
            .AddDefaultTokenProviders();

        services.AddTransient<IAuthService, AuthService>();
        services.AddTransient<IUserService, UserService>();

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(o =>
        {
            o.RequireHttpsMetadata = false;
            o.SaveToken = false;
            o.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero,
                ValidIssuer = configuration[Configs.JwtIssuer],
                ValidAudience = configuration[Configs.JwtAudience],
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration[Configs.JwtKey])
                )
            };
        });
        
        return services;
    }
}