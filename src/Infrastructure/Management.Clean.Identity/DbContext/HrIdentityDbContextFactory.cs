using Management.Clean.Identity.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Management.Clean.Identity.DbContext;

public class HrIdentityDbContextFactory : IDesignTimeDbContextFactory<HrIdentityDbContext>
    {
        public HrIdentityDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())  // where EF runs the command
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString(Configs.ConnectionString);

            var optionsBuilder = new DbContextOptionsBuilder<HrIdentityDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new HrIdentityDbContext(optionsBuilder.Options);
        }
    }
