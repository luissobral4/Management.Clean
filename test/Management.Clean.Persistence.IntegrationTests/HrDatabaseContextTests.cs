using Management.Clean.Persistence.DatabaseContext;
using Management.Clean.Persistence.IntegrationTests.Seed;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace Management.Clean.Persistence.IntegrationTests;

public class HrDatabaseContextTests
{
    private readonly HrDatabaseContext _hrDatabaseContext;

    public HrDatabaseContextTests()
    {
        var dbOptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase(databaseName: $"HrDb_{Guid.NewGuid()}")
            .Options;

        _hrDatabaseContext = new HrDatabaseContext(dbOptions);
    }

    [Fact]
    public async Task Save_SetDateCreatedValueTest()
    {
        // Arrange
        var leaveType = LeaveTypeSeed.GetLeaveType();

        // Act
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        // Assert
        leaveType.DateCreated.ShouldNotBeNull();
    }

    [Fact]
    public async Task Save_SetDateModifiedValueTest()
    {
        // Arrange
        var leaveType = LeaveTypeSeed.GetLeaveType();

        // Act
        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();

        // Assert
        leaveType.DateModified.ShouldNotBeNull();
    }
}