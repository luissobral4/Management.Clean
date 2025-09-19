namespace Management.Clean.Domain;

public class LeaveType : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public int DefaultDays { get; set; }
}

public struct LeaveTypeConstants
{
    public struct Props
    {
        public struct Name
        {
            public const int MAX_LENGTH = 70;
        }

        public struct DefaultDays
        {
            public const int MIN_LENGTH = 1;
            public const int MAX_LENGTH = 100;
        }
    }
}