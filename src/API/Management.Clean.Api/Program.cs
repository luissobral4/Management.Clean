using Management.Clean.Api.Constants;
using Management.Clean.Api.Middleware;
using Management.Clean.Application;
using Management.Clean.Identity;
using Management.Clean.Infrastructure;
using Management.Clean.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);

// Add services to the container.
builder.Services.AddControllers();

builder.Services.AddCors(option =>
{
    option.AddPolicy(Policies.CorsAllowAll, policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(Policies.CorsAllowAll);

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync();
