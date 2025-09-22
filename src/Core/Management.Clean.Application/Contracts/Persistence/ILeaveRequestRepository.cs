using Management.Clean.Domain;

namespace Management.Clean.Application.Contracts.Persistence;

public interface ILeaveRequestRepository : IGenericRepository<LeaveRequest>
{
    Task<LeaveRequest> GetLeaveRequestWithDetailsAsync(int id);
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync();
    Task<List<LeaveRequest>> GetLeaveRequestsWithDetailsAsync(string userId);

}
