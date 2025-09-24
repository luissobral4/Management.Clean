using FluentValidation;
using Management.Clean.Application.Features.Common;
using Management.Clean.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

namespace Management.Clean.Application.Features.LeaveRequest.Commands.ChangeLeaveRequestApproval;

public class ChangeLeaveRequestApprovalCommandValidator : AbstractValidator<ChangeLeaveRequestApprovalCommand>
{
    public ChangeLeaveRequestApprovalCommandValidator()
    {
        RuleFor(p => p.Approved)
            .NotNull()
            .WithMessage(ValidationMessages.PropertyRequired(nameof(LeaveRequestDto.Approved)));
    }
}