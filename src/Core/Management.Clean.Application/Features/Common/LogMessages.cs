namespace Management.Clean.Application.Features.Common;

public static class LogMessages
{
    private static string ObjectRetrieved(string type) => $"Leave {type} were retrieved successfully.";
    public static readonly string LeaveTypesRetrieved = ObjectRetrieved("Types");
    public static readonly string LeaveAllocationsRetrieved = ObjectRetrieved("Allocations");
    public static readonly string LeaveRequestsRetrieved = ObjectRetrieved("Requests");
}