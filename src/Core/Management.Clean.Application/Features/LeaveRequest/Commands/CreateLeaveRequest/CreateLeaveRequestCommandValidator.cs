using FluentValidation;
using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Application.Features.LeaveRequest.Shared;

namespace Management.Clean.Application.Features.LeaveRequest.Commands.CreateLeaveRequest;

public class CreateLeaveRequestCommandValidator : AbstractValidator<CreateLeaveRequestCommand>
{
    public CreateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        Include(new BaseLeaveRequestValidator(leaveTypeRepository));
    }
}