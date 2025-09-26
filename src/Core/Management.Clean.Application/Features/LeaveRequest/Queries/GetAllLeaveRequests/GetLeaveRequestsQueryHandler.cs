using Management.Clean.Application.Contracts.Persistence;
using MediatR;
using AutoMapper;
using Management.Clean.Application.Contracts.Logging;
using Management.Clean.Application.Features.Common;
using Management.Clean.Application.Contracts.Identity;

namespace Management.Clean.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public class GetLeaveRequestsQueryHandler : IRequestHandler<GetLeaveRequestsQuery, List<LeaveRequestDto>>
{
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ILeaveRequestRepository _leaveRequestRepository;
    private readonly IAppLogger<GetLeaveRequestsQueryHandler> _logger;

    public GetLeaveRequestsQueryHandler(
        IMapper mapper,
        IUserService userService,
        ILeaveRequestRepository leaveRequestRepository,
        IAppLogger<GetLeaveRequestsQueryHandler> logger)
    {
        _mapper = mapper;
        _userService = userService;
        _leaveRequestRepository = leaveRequestRepository;
        _logger = logger;
    }

    public async Task<List<LeaveRequestDto>> Handle(GetLeaveRequestsQuery request, CancellationToken cancellationToken)
    {
        List<Domain.LeaveRequest> leaveRequests;
        List<LeaveRequestDto> requests;

        if (request.IsLoggedInUser)
        {
            var userId = _userService.UserId;
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetailsAsync(userId);

            var employee = await _userService.GetEmployeeAsync(userId);
            requests = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

            foreach (var req in requests)
            {
                req.Employee = employee;
            }
        }
        else
        {
            leaveRequests = await _leaveRequestRepository.GetLeaveRequestsWithDetailsAsync();
            requests = _mapper.Map<List<LeaveRequestDto>>(leaveRequests);

            foreach (var req in requests)
            {
                req.Employee = await _userService.GetEmployeeAsync(req.RequestingEmployeeId);
            }

        }

        _logger.LogInformation(LogMessages.LeaveRequestsRetrieved);

        return requests;
    }
}