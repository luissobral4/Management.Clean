using MediatR;
using AutoMapper;
using Management.Clean.Application.Exceptions;
using Management.Clean.Application.Contracts.Persistence;

namespace Management.Clean.Application.Features.LeaveAllocation.Queries.GetLeaveAllocationDetails;

public class GetLeaveAllocationDetailsQueryHandler : IRequestHandler<GetLeaveAllocationDetailsQuery, LeaveAllocationDetailsDto>
{
    private readonly IMapper _mapper;
    private readonly ILeaveAllocationRepository _leaveAllocationsRepository;

    public GetLeaveAllocationDetailsQueryHandler(IMapper mapper, ILeaveAllocationRepository leaveAllocationsRepository)
    {
        _mapper = mapper;
        _leaveAllocationsRepository = leaveAllocationsRepository;
    }

    public async Task<LeaveAllocationDetailsDto> Handle(GetLeaveAllocationDetailsQuery request, CancellationToken cancellationToken)
    {
        var leaveAllocations = await _leaveAllocationsRepository.GetLeaveAllocationWithDetailsAsync(request.Id);

        if (leaveAllocations == null)
        {
            throw new NotFoundException(nameof(LeaveAllocation), request.Id);
        }

        var data = _mapper.Map<LeaveAllocationDetailsDto>(leaveAllocations);

        return data;
    }
}