using FluentValidation;
using Management.Clean.Application.Constants;
using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Application.Features.Common;
using Management.Clean.Application.Features.LeaveRequest.Shared;

namespace Management.Clean.Application.Features.LeaveRequest.Commands.UpdateLeaveRequest;

public class UpdateLeaveRequestCommandValidator : AbstractValidator<UpdateLeaveRequestCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveRequestRepository _leaveRequestRepository;

    public UpdateLeaveRequestCommandValidator(ILeaveTypeRepository leaveTypeRepository, ILeaveRequestRepository leaveRequestRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveRequestRepository = leaveRequestRepository;

        RuleFor(p => p.Id)
            .NotNull()
            .MustAsync(LeaveRequestMustExist)
            .WithMessage(ValidationMessages.PropertyRequired(Properties.Id)); ;

        Include(new BaseLeaveRequestValidator(_leaveTypeRepository));
    }
    
    private async Task<bool> LeaveRequestMustExist(int id, CancellationToken token)
    {
        var leaveType = await _leaveRequestRepository.GetByIdAsync(id);

        return leaveType != null;
    }
}