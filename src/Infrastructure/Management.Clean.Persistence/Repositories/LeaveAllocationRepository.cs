using Management.Clean.Application.Contracts.Persistence;
using Management.Clean.Domain;
using Management.Clean.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace Management.Clean.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation>, ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task AddAllocationsAsync(List<LeaveAllocation> allocations)
    {
        await _context.AddRangeAsync(allocations);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> AllocationExistsAsync(string userId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId
                            && q.LeaveTypeId == leaveTypeId
                            && q.Period == period);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync()
    {
        var leaveAllocations = await _context.LeaveAllocations
            .Include(q => q.LeaveType)
            .ToListAsync();

        return leaveAllocations;
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationsWithDetailsAsync(string userId)
    {
        var leaveAllocations = await _context.LeaveAllocations
            .Where(q => q.EmployeeId == userId)
            .Include(q => q.LeaveType)
            .ToListAsync();

        return leaveAllocations;
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetailsAsync(int id)
    {
        var leaveAllocation = await _context.LeaveAllocations.FirstOrDefaultAsync(q => q.Id == id);

        return leaveAllocation;
    }

    public async Task<LeaveAllocation> GetUserAllocationsAsync(string userId, int leaveTypeId)
    {
        var leaveAllocation = await _context.LeaveAllocations.FirstOrDefaultAsync(q =>
            q.LeaveTypeId == leaveTypeId &&
            q.EmployeeId == userId
        );

        return leaveAllocation;
    }
}