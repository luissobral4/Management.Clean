using MediatR;

namespace Management.Clean.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommand : IRequest<Unit>
{
    public int Id { get; set; }
}