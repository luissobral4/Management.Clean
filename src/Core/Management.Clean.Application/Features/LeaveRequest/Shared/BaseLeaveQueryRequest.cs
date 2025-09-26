using Management.Clean.Application.Features.LeaveType.Queries.GetAllLeaveTypes;
using Management.Clean.Application.Models.Identity;

namespace Management.Clean.Application.Features.LeaveRequest.Shared;

public abstract class BaseLeaveQueryRequest
{
    public int Id { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public LeaveTypeDto LeaveType { get; set; }
    public DateTime DateRequested { get; set; }
    public bool? Approved { get; set; }
    public bool? Cancelled { get; set; }
    public string RequestingEmployeeId { get; set; }
    public Employee Employee { get; set; }
}