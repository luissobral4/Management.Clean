using Management.Clean.Application.Models.Identity;

namespace Management.Clean.Application.Contracts.Identity;

public interface IUserService
{
    Task<List<Employee>> GetEmployeesAsync();
    Task<Employee> GetEmployeeAsync(string userId);
    public string UserId { get; }
}