using Management.Clean.Application.Features.LeaveRequest.Shared;
using Management.Clean.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

namespace Management.Clean.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class LeaveRequestDto : BaseLeaveRequest
{
   public LeaveTypeDto LeaveType { get; set; }
   public DateTime DateRequested { get; set; }
   public bool? Approved { get; set; }
   public string RequestingEmployeeId { get; set; }
}