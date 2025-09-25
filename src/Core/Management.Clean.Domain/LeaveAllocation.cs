namespace Management.Clean.Domain;

public class LeaveAllocation : BaseEntity
{
    public int NumberOfDays { get; set; }
    public int LeaveTypeId { get; set; }
    public LeaveType? LeaveType { get; set; }
    public int Period { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
}

public struct LeaveAllocationConstants
{
    public struct Props
    {
        public struct LeaveTypeId
        {
            public const int MIN_LENGTH = 0;
        }

        public struct NumberOfDays
        {
            public const int MIN_LENGTH = 0;
        }
    }
}