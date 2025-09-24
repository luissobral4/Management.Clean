using FluentValidation;
using Management.Clean.Application.Constants;
using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Application.Features.Common;
using Management.Clean.Domain;

namespace Management.Clean.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandValidator : AbstractValidator<CreateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;

        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(LeaveAllocationConstants.Props.LeaveTypeId.MIN_LENGTH)
            .WithMessage(ValidationMessages.PropertyMinSize(Properties.LeaveTypeId, LeaveAllocationConstants.Props.LeaveTypeId.MIN_LENGTH))
            .MustAsync(LeaveTypeMustExists)
            .WithMessage(ValidationMessages.ObjectDontExists(nameof(LeaveType)));
    }

    private async Task<bool> LeaveTypeMustExists(int id, CancellationToken token)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);

        return leaveType != null;
    }
}