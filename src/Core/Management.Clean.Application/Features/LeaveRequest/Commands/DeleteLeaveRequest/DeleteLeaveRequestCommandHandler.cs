using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Application.Exceptions;
using MediatR;

namespace Management.Clean.Application.Features.LeaveRequest.Commands.DeleteLeaveRequest;

public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand>
{
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public DeleteLeaveRequestCommandHandler(ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveRequestRepository = leaveRequestRepository;
    }
    public async Task Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
    {
        var leaveRequestToDelete = await _leaveRequestRepository.GetByIdAsync(request.Id);

        if (leaveRequestToDelete == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        await _leaveRequestRepository.DeleteAsync(leaveRequestToDelete);
    }
}