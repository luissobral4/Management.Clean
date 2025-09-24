using MediatR;

namespace Management.Clean.Application.Features.LeaveRequest.Queries.GetAllLeaveRequests;

public record GetLeaveRequestsQuery : IRequest<List<LeaveRequestDto>>;