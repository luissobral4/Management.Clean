using System.Security.Claims;
using Management.Clean.Application.Contracts.Identity;
using Management.Clean.Application.Models.Identity;
using Management.Clean.Identity.Constants;
using Management.Clean.Identity.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Management.Clean.Identity.Services;

public class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public UserService(UserManager<ApplicationUser> userManager, IHttpContextAccessor httpContextAccessor)
    {
        _userManager = userManager;
        _httpContextAccessor = httpContextAccessor;
    }

    public string UserId { get => _httpContextAccessor.HttpContext?.User?.FindFirstValue(Configs.UidClaim); }

    public async Task<Employee> GetEmployeeAsync(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);

        return MapToEmployee(user);
    }

    public async Task<List<Employee>> GetEmployeesAsync()
    {
        var users = await _userManager.GetUsersInRoleAsync(Roles.Employee);

        return users.Select(MapToEmployee).ToList();
    }

    private Employee MapToEmployee(ApplicationUser user)
    {
        return new Employee
        {
            Id = user.Id,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Email = user.Email
        };
    }
}