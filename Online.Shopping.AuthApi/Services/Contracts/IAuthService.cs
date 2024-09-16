using Online.Shopping.AuthApi.Models.Dtos;

namespace Online.Shopping.AuthApi.Services.Contracts
{
    public interface IAuthService
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
        Task<bool> AssignRoleAsync(string email, string roleName);
    }
}
