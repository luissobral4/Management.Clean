using FluentValidation;
using Management.Clean.Application.Constants;
using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Application.Features.Common;
using Management.Clean.Domain;

namespace Management.Clean.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator : AbstractValidator<UpdateLeaveAllocationCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public UpdateLeaveAllocationCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .WithMessage(ValidationMessages.PropertyRequired(Properties.Id))
            .MustAsync(LeaveAllocationMustExist); ;

        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(LeaveAllocationConstants.Props.LeaveTypeId.MIN_LENGTH)
            .WithMessage(ValidationMessages.PropertyMinSize(Properties.LeaveTypeId, LeaveAllocationConstants.Props.LeaveTypeId.MIN_LENGTH))
            .MustAsync(LeaveTypeMustExists)
            .WithMessage(ValidationMessages.ObjectDontExists(nameof(LeaveType)));

        RuleFor(p => p.Period)
            .GreaterThanOrEqualTo(DateTime.Now.Year)
            .WithMessage(ValidationMessages.DateMustBeAfter(Properties.Period, DateTime.Now.Year.ToString()));

        RuleFor(p => p.NumberOfDays)
            .GreaterThan(LeaveAllocationConstants.Props.NumberOfDays.MIN_LENGTH)
            .WithMessage(ValidationMessages.PropertyMinSize(Properties.NumberOfDays, LeaveAllocationConstants.Props.NumberOfDays.MIN_LENGTH));
    }

    private async Task<bool> LeaveTypeMustExists(int id, CancellationToken token)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);

        return leaveType != null;
    }
    
    private async Task<bool> LeaveAllocationMustExist(int id, CancellationToken token)
    {
        var leaveType = await _leaveAllocationRepository.GetByIdAsync(id);

        return leaveType != null;
    }
}