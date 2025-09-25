using Management.Clean.Application.Models.Identity;

namespace Management.Clean.Application.Contracts.Identity;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(AuthRequest request);
    Task<RegistationResponse> RegisterAsync(RegistationRequest request);
}