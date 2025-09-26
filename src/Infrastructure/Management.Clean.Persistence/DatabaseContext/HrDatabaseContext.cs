using Management.Clean.Application.Contracts.Identity;
using Management.Clean.Domain;
using Microsoft.EntityFrameworkCore;

namespace Management.Clean.Persistence.DatabaseContext;

public class HrDatabaseContext : DbContext
{
    private readonly IUserService _userService;
    public HrDatabaseContext(DbContextOptions<HrDatabaseContext> options, IUserService userService) : base(options)
    {
        _userService = userService;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDatabaseContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }

    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
            .Where(q => q.State == EntityState.Added || q.State == EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.UtcNow;
            entry.Entity.ModifiedBy = _userService.UserId;

            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.UtcNow;
                entry.Entity.CreatedBy = _userService.UserId;
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}