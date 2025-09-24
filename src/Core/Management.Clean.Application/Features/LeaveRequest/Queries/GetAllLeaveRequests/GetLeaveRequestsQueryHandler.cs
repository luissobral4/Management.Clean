using Management.Clean.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using Management.Clean.Application.Contracts.Logging;
using Management.Clean.Application.Features.Common;

namespace Management.Clean.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class GetLeaveRequestsQueryHandler : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestDto>>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<GetLeaveRequestsQueryHandler> _logger;

    public GetLeaveRequestsQueryHandler(
        IMapper mapper,
        ILeaveRequestRepository leaveRequestRepository,
        IAppLogger<GetLeaveRequestsQueryHandler> logger)
    {
        _mapper = mapper;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
    }

    public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
    {
        var leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetailsAsync();

        var requests = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

        _logger.LogInformation(LogMessages.LeaveRequestsRetrieved);

        return requests;
    }
}