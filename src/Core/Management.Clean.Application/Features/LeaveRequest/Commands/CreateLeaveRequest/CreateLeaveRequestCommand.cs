using Management.Clean.Application.Features.LeaveRequest.Shared;
using MediatR;

namespace Management.Clean.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommand : BaseLeaveRequest, IRequest<Unit>
{
    public string RequestComments { get; set; }
}