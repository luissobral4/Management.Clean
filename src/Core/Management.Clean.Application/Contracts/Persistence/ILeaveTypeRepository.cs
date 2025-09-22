using Management.Clean.Domain;

namespace Management.Clean.Application.Contracts.Persistence;

public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{
    Task<bool> IsLeaveTypeUniqueAsync(string name);
}
