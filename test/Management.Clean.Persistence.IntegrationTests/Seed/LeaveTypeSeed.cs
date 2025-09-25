using Management.Clean.Domain;

namespace Management.Clean.Persistence.IntegrationTests.Seed;

public static class LeaveTypeSeed
{
    private static LeaveType _leaveType = new LeaveType
    {
        Id = 1,
        DefaultDays = 10,
        Name = "Test Vacation"
    };

    public static LeaveType GetLeaveType()
    {
        return _leaveType;
    }
}