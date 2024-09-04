using Microsoft.AspNetCore.Identity;
using Online.Shopping.AuthApi.Data;
using Online.Shopping.AuthApi.Models;
using Online.Shopping.AuthApi.Models.Dtos;
using Online.Shopping.AuthApi.Services.Contracts;

namespace Online.Shopping.AuthApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

        
        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            throw new NotImplementedException();
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            ApplicationUser user = new()
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                NormalizedEmail = registerDto.Email.ToUpper(),
                Name = registerDto.Name,
                PhoneNumber = registerDto.PhoneNumber
            };

            try
            {
                var result = await _userManager.CreateAsync(user, registerDto.Password);
                if (result.Succeeded)
                {
                    var userResponse = _context.ApplicationUsers.First(u => u.UserName == registerDto.Email);

                    UserDto userDto = new()
                    {
                        Email = userResponse.Email,
                        Id = userResponse.Id,
                        Name = userResponse.Name,
                        PhoneNumber = userResponse.PhoneNumber
                    };

                    return null;
                }
                else
                {
                    return result.Errors.FirstOrDefault().Description;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
            //return "An Error Encountered";
        }
    }
}
