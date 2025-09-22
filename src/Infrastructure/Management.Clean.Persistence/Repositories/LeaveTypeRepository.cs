using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Domain;
using Management.Clean.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Management.Clean.Persistence.Repositories;

public class LeaveTypeRepository : GenericRepository<LeaveType>, ILeaveTypeRepository
{
    public LeaveTypeRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<bool> IsLeaveTypeUniqueAsync(string name)
    {
        return !await _context.LeaveTypes.AnyAsync(lt => lt.Name == name);
    }
}