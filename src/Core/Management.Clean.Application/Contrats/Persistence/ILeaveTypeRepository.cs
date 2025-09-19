using Management.Clean.Domain;

namespace Management.Clean.Application.Contrats.Persistence;

public interface ILeaveTypeRepository : IGenericRepository<LeaveType>
{
    Task<bool> IsLeaveTypeUniqueAsync(string name);
}
