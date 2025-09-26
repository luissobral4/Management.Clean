using Management.Clean.Application.Features.LeaveRequest.Shared;

namespace Management.Clean.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class LeaveRequestDetailsDto : BaseLeaveRequest
{
    public string? RequestComments { get; set; }
    public bool Cancelled { get; set; }
    public DateTime? DateActioned { get; set; }
}