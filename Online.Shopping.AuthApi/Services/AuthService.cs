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
        private readonly IGenerateJwtToken _generateJwtToken;

        public AuthService(ApplicationDbContext context, UserManager<ApplicationUser> userManager, 
            RoleManager<IdentityRole> roleManager, 
            IGenerateJwtToken generateJwtToken)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
            _generateJwtToken = generateJwtToken ?? throw new ArgumentNullException(nameof(generateJwtToken));
        }

        
        public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(x => x.UserName == loginDto.UserName.ToLower());

            bool isPasswordValid = await _userManager.CheckPasswordAsync(user, loginDto.Password);

            if(user == null || isPasswordValid == false)
            {
                return new LoginResponseDto() { User = null, Token = "" };
            }

            // If user is valid, generate the token
            var token = _generateJwtToken.GenerateToken(user);

            UserDto userDto = new()
            {
                Id = user.Id,
                Email = user.Email,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber
            };

            LoginResponseDto loginResponseDto = new LoginResponseDto()
            {
                User = userDto,
                Token = token
            };

            return loginResponseDto;
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

        public async Task<bool> AssignRoleAsync(string email, string roleName)
        {
            var user = _context.ApplicationUsers.FirstOrDefault(x => x.Email.ToLower() == email.ToLower());

            if(user is not null)
            {
                if (!_roleManager.RoleExistsAsync(roleName).GetAwaiter().GetResult())
                {
                    // create role if it does not exit
                    _roleManager.CreateAsync(new IdentityRole(roleName)).GetAwaiter().GetResult();
                }

                await _userManager.AddToRoleAsync(user, roleName);
                return true;
            }
            return false;
        }
    }
}
