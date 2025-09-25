using Management.Clean.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Management.Clean.Identity.Constants;

public static class Seeds
{
    private static PasswordHasher<ApplicationUser> _hasher = new PasswordHasher<ApplicationUser>();

    private static IdentityRole role1 = BuildRole("1050fbb7-046c-483e-81e4-462e01016c52", "Admin", "ADMIN");

    private static IdentityRole role2 = BuildRole("f7a57a46-c7e5-433b-9f89-eced9ff84e36", "Employee", "EMPLOYEE");

    private static ApplicationUser user1 = BuildUser("a2fb4e59-c3f1-4114-b892-2b5579e25f5f", "admin@localhost.com", "System", "Admin");

    private static ApplicationUser user2 = BuildUser("feeb9cb4-38ca-4fc2-a1a4-a7701c8cfad5", "user@localhost.com", "System", "User");

    private static IdentityUserRole<string> userRole1 = BuildUserRole(user1.Id, role1.Id);

    private static IdentityUserRole<string> userRole2 = BuildUserRole(user2.Id, role2.Id);

    private static IdentityRole BuildRole(string id, string name, string normalizedName) =>
        new IdentityRole
        {
            Id = id,
            Name = name,
            NormalizedName = normalizedName,
            ConcurrencyStamp = Guid.NewGuid().ToString()
        };

    private static ApplicationUser BuildUser(string id, string email, string firstName, string lastName, string password = "P@ssword1") =>
        new ApplicationUser
        {
            Id = id,
            Email = email,
            NormalizedEmail = email.ToUpper(),
            UserName = email,
            NormalizedUserName = email.ToUpper(),
            FirstName = firstName,
            LastName = lastName,
            PasswordHash = _hasher.HashPassword(null, password),
            EmailConfirmed = true
        };

    private static IdentityUserRole<string> BuildUserRole(string userId, string roleId) =>
        new IdentityUserRole<string>
        {
            UserId = userId,
            RoleId = roleId
        };

    public static IReadOnlyList<ApplicationUser> Users { get; } = new List<ApplicationUser> { user1, user2 };

    public static IReadOnlyList<IdentityRole> Roles { get; } = new List<IdentityRole> { role1, role2 };
    
    public static IReadOnlyList<IdentityUserRole<string>> UserRoles { get; } = new List<IdentityUserRole<string>> { userRole1, userRole2 };
}