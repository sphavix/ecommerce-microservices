using Online.Shopping.WebApp.Models;
using Online.Shopping.WebApp.Models.AuthDtos;
using Online.Shopping.WebApp.Services.Contracts;
using Online.Shopping.WebApp.Utilities;

namespace Online.Shopping.WebApp.Services
{
    public class AuthService : IAuthService
    {
        private readonly IBaseService _baseService;

        public AuthService(IBaseService baseService)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
        }
        public async Task<ResponseDto> AssignRoleAsync(RegisterDto registerDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ServiceDependency.ApiType.POST,
                Data = registerDto, // Dto is serialized here in the BaseService
                ApiUrl = ServiceDependency.AuthApiBaseUrl + "/api/auth/AssignRole"
            });
        }

        public async Task<ResponseDto> LoginAsync(LoginDto loginDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ServiceDependency.ApiType.POST,
                Data = loginDto, // Dto is serialized here in the BaseService
                ApiUrl = ServiceDependency.AuthApiBaseUrl + "/api/auth/login"
            });
        }

        public async Task<ResponseDto> RegisterAsync(RegisterDto registerDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ServiceDependency.ApiType.POST,
                Data = registerDto, // Dto is serialized here in the BaseService
                ApiUrl = ServiceDependency.AuthApiBaseUrl + "/api/auth/register"
            });
        }
    }
}
