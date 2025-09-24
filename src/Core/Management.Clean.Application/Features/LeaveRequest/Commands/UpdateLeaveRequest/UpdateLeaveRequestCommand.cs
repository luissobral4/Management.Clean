using Management.Clean.Application.Features.LeaveRequest.Shared;
using MediatR;

namespace Management.Clean.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
{
    public int Id { get; set; }
    public string? RequestComments { get; set; }
    public bool Cancelled { get; set; }
}