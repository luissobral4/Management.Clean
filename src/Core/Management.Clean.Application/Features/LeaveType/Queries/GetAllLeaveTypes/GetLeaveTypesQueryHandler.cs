using Management.Clean.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using Management.Clean.Application.Contracts.Logging;
using Management.Clean.Application.Features.Common;

namespace Management.Clean.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public class GetLeaveTypesQueryHandler : IRequestHandler<GetLeaveTypesQuery, List<LeaveTypeDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveTypeRepository _leaveTypeRepository;
    private readonly IAppLogger<GetLeaveTypesQueryHandler> _logger;

    public GetLeaveTypesQueryHandler(
        IMapper mapper,
        ILeaveTypeRepository leaveTypeRepository,
        IAppLogger<GetLeaveTypesQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveTypeRepository = leaveTypeRepository;
        _logger = logger;
    }

    public async Task<List<LeaveTypeDto>> Handle(GetLeaveTypesQuery request, CancellationToken cancellationToken)
    {
        var leaveTypes = await _leaveTypeRepository.GetAsync();

        var data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);

        _logger.LogInformation(LogMessages.LeaveTypesRetrieved);

        return data;
    }
}