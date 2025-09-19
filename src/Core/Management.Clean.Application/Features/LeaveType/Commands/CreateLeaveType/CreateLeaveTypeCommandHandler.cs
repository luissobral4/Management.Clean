using AutoMapper;
using Management.Clean.Application.Contrats.Persistence;
using Management.Clean.Application.Exceptions;
using Management.Clean.Application.Features.Common;
using MediatR;

namespace Management.Clean.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, int>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandHandler(IMapper mapper, ILeaveTypeRepository leaveTypeRepository)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<int> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var validator = new CreateLeaveTypeCommandValidator(_leaveTypeRepository);
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.Errors.Any())
            throw new NotFoundException(ValidationMessages.ObjectInvalid(nameof(LeaveType)), validationResult);

        var leaveTypeToCreate = _mapper.Map<Domain.LeaveType>(request);

        await _leaveTypeRepository.CreateAsync(leaveTypeToCreate);

        return leaveTypeToCreate.Id;
    }
}