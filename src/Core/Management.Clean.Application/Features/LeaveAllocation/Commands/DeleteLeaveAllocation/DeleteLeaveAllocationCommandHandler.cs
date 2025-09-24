using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Application.Exceptions;
using MediatR;

namespace Management.Clean.Application.Features.LeaveAllocation.Commands.DeleteLeaveAllocation;

public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, Unit>
{
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public DeleteLeaveAllocationCommandHandler(ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveAllocationRepository = leaveAllocationRepository;
    }
    public async Task<Unit> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var leaveAllocationToDelete = await _leaveAllocationRepository.GetByIdAsync(request.Id);

        if (leaveAllocationToDelete == null)
        {
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);
        }

        await _leaveAllocationRepository.DeleteAsync(leaveAllocationToDelete);

        return Unit.Value;
    }
}