using FluentValidation;
using Management.Clean.Application.Constants;
using Management.Clean.Application.Features.Common;

namespace Management.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryValidator : AbstractValidator<GetLeaveAllocationDetailsQuery>
{
    public GetLeaveAllocationDetailsQueryValidator()
    {
        RuleFor(p => p.Id)
            .NotEmpty().WithMessage(ValidationMessages.PropertyRequired(Properties.Id))
            .NotNull();
    }
}