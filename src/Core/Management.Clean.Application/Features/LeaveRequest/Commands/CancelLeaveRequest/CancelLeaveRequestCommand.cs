using MediatR;

namespace Management.Clean.Application.Features.LeaveRequest.Commands.CancelLeaveRequest;

public class CancelLeaveRequestCommand : IRequest
{
    public int Id { get; set; }
}