using Core.Dtos.Account;

namespace Frontend.Services
{
    public interface IAuthService
    {
        Task<LoginResultDto> LoginAsync(LoginRequestDto loginRequest);
        Task LogoutAsync();
    }
}
