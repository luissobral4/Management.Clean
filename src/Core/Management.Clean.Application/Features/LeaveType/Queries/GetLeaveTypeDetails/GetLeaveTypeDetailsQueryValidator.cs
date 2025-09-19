using FluentValidation;
using Management.Clean.Application.Constants;
using Management.Clean.Application.Features.Common;

namespace Management.Clean.Application.Features.LeaveType.Queries.GetLeaveTypeDetails;

public class GetLeaveTypeDetailsQueryValidator : AbstractValidator<GetLeaveTypeDetailsQuery>
{
    public GetLeaveTypeDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage(ValidationMessages.PropertyRequired(Properties.Id))
            .NotNull();
    }
}