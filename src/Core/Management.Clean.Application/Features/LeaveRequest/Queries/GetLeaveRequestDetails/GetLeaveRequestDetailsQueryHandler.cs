using MediatR;
using AutoMapper;
using Management.Clean.Application.Exceptions;
using Management.Clean.Application.Contracts.Persistence;

namespace Management.Clean.Application.Features.LeaveRequest.Queries.GetLeaveRequestDetails;

public class GetLeaveRequestDetailsQueryHandler : IRequestHandler<GetLeaveRequestDetailsQuery, LeaveRequestDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveRequestRepository _leaveRequestsRepository;

    public GetLeaveRequestDetailsQueryHandler(IMapper mapper, ILeaveRequestRepository leaveRequestsRepository)
    {
        _mapper = mapper;
        _leaveRequestsRepository = leaveRequestsRepository;
    }

    public async Task<LeaveRequestDetailsDto> Handle(GetLeaveRequestDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveRequest = await _leaveRequestsRepository.GetLeaveRequestWithDetailsAsync(request.Id);

        if (leaveRequest == null)
        {
            throw new NotFoundException(nameof(LeaveRequest), request.Id);
        }

        var data = _mapper.Map<LeaveRequestDetailsDto>(leaveRequest);

        return data;
    }
}