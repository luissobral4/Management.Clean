using Management.Clean.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Management.Clean.Persistence.Configurations;

public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
{
    public void Configure(EntityTypeBuilder<LeaveType> modelBuilder)
    {
        modelBuilder.HasData(
            new LeaveType
            {
                Id = 1,
                Name = "Vacation",
                DefaultDays = 10,
                DateCreated = DateTime.UtcNow,
                DateModified = DateTime.UtcNow
            }
        );

        modelBuilder.Property(q => q.Name)
            .IsRequired()
            .HasMaxLength(100);
    }
}