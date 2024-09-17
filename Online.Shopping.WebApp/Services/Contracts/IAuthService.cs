using Online.Shopping.WebApp.Models;
using Online.Shopping.WebApp.Models.AuthDtos;

namespace Online.Shopping.WebApp.Services.Contracts
{
    public interface IAuthService
    {
        Task<ResponseDto?> LoginAsync(LoginDto loginDto);
        Task<ResponseDto?> RegisterAsync(RegisterDto registerDto);
        Task<ResponseDto?> AssignRoleAsync(RegisterDto registerDto);
    }
}
