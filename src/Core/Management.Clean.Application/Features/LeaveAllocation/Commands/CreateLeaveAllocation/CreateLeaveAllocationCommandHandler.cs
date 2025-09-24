using AutoMapper;
using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Application.Exceptions;
using Management.Clean.Application.Features.Common;
using MediatR;

namespace Management.Clean.Application.Features.LeaveAllocation.Commands.CreateLeaveAllocation;

public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, Unit>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;

    public CreateLeaveAllocationCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository, ILeaveAllocationRepository leaveAllocationRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _leaveAllocationRepository = leaveAllocationRepository;
    }
    public async Task<Unit> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveAllocationCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new BadRequestException(ValidationMessages.ObjectInvalid(nameof(LeaveAllocation)), validationResult);

        var leaveType = await _leaveTypeRepository.GetByIdAsync(request.LeaveTypeId);

        var leaveAllocationToCreate = _mapper.Map<Domain.LeaveAllocation>(request);

        await _leaveAllocationRepository.CreateAsync(leaveAllocationToCreate);

        return Unit.Value;
    }
}