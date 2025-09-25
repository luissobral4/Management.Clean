using Management.Clean.Application.Contracts.Identity;
using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Application.Exceptions;
using Management.Clean.Application.Features.Common;
using MediatR;

namespace Management.Clean.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IUserService _userService;

    public CreateLeaveAllocationCommandHandler(
        ILeaveTypeRepository leaveTypeRepository,
        ILeaveAllocationRepository leaveAllocationRepository,
        IUserService userService
    )
    {
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
        _userService = userService;
    }
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException(ValidationMessages.ObjectInvalid(nameof(LeaveAllocation)), validationResult);

        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        var employees = await _userService.GetEmployeesAsync();

        var period = DateTime.Now.Year;

        var allocations = new List<Domain.LeaveAllocation>();

        foreach (var employee in employees)
        {
            var allocationExists = await _leaveAllocationRepository.AllocationExistsAsync(
                employee.Id,
                leaveType.Id,
                period);

            if (!allocationExists)
            {
                allocations.Add(new Domain.LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = request.LeaveTypeId,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period
                });
            }
        }

        if (allocations.Count > 0)
        {
            await _leaveAllocationRepository.AddAllocationsAsync(allocations);
        }

        return Unit.Value;
    }
}