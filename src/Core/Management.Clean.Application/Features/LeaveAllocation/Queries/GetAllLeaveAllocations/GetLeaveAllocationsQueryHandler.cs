using Management.Clean.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using Management.Clean.Application.Contracts.Logging;
using Management.Clean.Application.Features.Common;

namespace Management.Clean.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public class GetLeaveAllocationsQueryHandler : IRequestHandler<GetLeaveAllocationsQuery, List<LeaveAllocationDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationRepository;
    private readonly IAppLogger<GetLeaveAllocationsQueryHandler> _logger;

    public GetLeaveAllocationsQueryHandler(
        IMapper mapper,
        ILeaveAllocationRepository leaveAllocationRepository,
        IAppLogger<GetLeaveAllocationsQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveAllocationRepository = leaveAllocationRepository;
        _logger = logger;
    }

    public async Task<List<LeaveAllocationDto>> Handle(GetLeaveAllocationsQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocations = await _leaveAllocationRepository.GetLeaveAllocationsWithDetailsAsync();

        var allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

        _logger.LogInformation(LogMessages.LeaveAllocationsRetrieved);

        return allocations;
    }
}