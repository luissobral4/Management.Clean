using Management.Clean.Application.Features.LeaveRequest.Shared;
using Management.Clean.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace Management.Clean.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class LeaveRequestDetailsDto : BaseLeaveRequest
{
    public LeaveTypeDto LeaveType { get; set; }
    public DateTime DateRequested { get; set; }
    public string? RequestComments { get; set; }
    public bool? Approved { get; set; }
    public bool Cancelled { get; set; }
    public string RequestingEmployeeId { get; set; }
}