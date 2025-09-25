using FluentValidation;
using Management.Clean.Application.Constants;
using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Application.Features.Common;
using Management.Clean.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;
using Management.Clean.Domain;

namespace Management.Clean.Application.Features.LeaveRequest.Shared;

public class BaseLeaveRequestValidator : AbstractValidator<BaseLeaveRequest>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public BaseLeaveRequestValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;


        RuleFor(p => p.LeaveTypeId)
            .GreaterThan(LeaveRequestConstants.Props.LeaveTypeId.MIN_LENGTH)
            .WithMessage(ValidationMessages.PropertyMinSize(Properties.LeaveTypeId, LeaveRequestConstants.Props.LeaveTypeId.MIN_LENGTH))
            .MustAsync(LeaveTypeMustExists)
            .WithMessage(ValidationMessages.ObjectDontExists(nameof(LeaveType)));

        RuleFor(p => p.StartDate)
            .LessThan(p => p.EndDate)
            .WithMessage(ValidationMessages.DateMustBeBefore(nameof(LeaveRequestDto.StartDate), nameof(LeaveRequestDto.EndDate)));

        RuleFor(p => p.EndDate)
            .GreaterThan(p => p.StartDate)
            .WithMessage(ValidationMessages.DateMustBeBefore(nameof(LeaveRequestDto.EndDate), nameof(LeaveRequestDto.StartDate)));
    }
    
    private async Task<bool> LeaveTypeMustExists(int id, CancellationToken token)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);

        return leaveType != null;
    }
}