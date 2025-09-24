using MediatR;

namespace Management.Clean.Application.Features.LeaveAllocation.Queries.GetAllLeaveAllocations;

public record GetLeaveAllocationsQuery : IRequest<List<LeaveAllocationDto>>;