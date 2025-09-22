using FluentValidation;
using Management.Clean.Application.Constants;
using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Application.Features.Common;
using Management.Clean.Domain;

namespace Management.Clean.Application.Features.LeaveType.Commands.UpdateLeaveType;

public class UpdateLeaveTypeCommandValidator : AbstractValidator<UpdateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public UpdateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(leaveTypeMustExist);

        RuleFor(p => p.Name)
            .NotEmpty().WithMessage(ValidationMessages.PropertyRequired(Properties.Name))
            .NotNull()
            .MaximumLength(LeaveTypeConstants.Props.Name.MAX_LENGTH).WithMessage(ValidationMessages.PropertyMaxSize(Properties.Name, LeaveTypeConstants.Props.Name.MAX_LENGTH));

        RuleFor(p => p.DefaultDays)
            .GreaterThan(LeaveTypeConstants.Props.DefaultDays.MIN_LENGTH).WithMessage(ValidationMessages.PropertyMinSize(Properties.DefaultDays, LeaveTypeConstants.Props.DefaultDays.MIN_LENGTH))
            .LessThan(LeaveTypeConstants.Props.DefaultDays.MAX_LENGTH).WithMessage(ValidationMessages.PropertyMaxSize(Properties.DefaultDays, LeaveTypeConstants.Props.DefaultDays.MAX_LENGTH));

        RuleFor(q => q)
            .MustAsync(LeaveTypeNameUnique)
            .WithMessage(ValidationMessages.ObjectAlreadyExists(nameof(LeaveType)));

        _leaveTypeRepository = leaveTypeRepository;
    }

    private async Task<bool> leaveTypeMustExist(int id, CancellationToken token)
    {
        var leaveType = await _leaveTypeRepository.GetByIdAsync(id);

        return leaveType != null;
    }

    private Task<bool> LeaveTypeNameUnique(UpdateLeaveTypeCommand command, CancellationToken token)
    {
        return _leaveTypeRepository.IsLeaveTypeUniqueAsync(command.Name);
    }
}