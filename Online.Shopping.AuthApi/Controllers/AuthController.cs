using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online.Shopping.AuthApi.Models.Dtos;
using Online.Shopping.AuthApi.Services.Contracts;

namespace Online.Shopping.AuthApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        protected ResponseDto _response;

        public AuthController(IAuthService authService)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _response = new();
        }



        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto register)
        {
            var errorMessage = await _authService.RegisterAsync(register);
            if(!string.IsNullOrEmpty(errorMessage))
            {
                _response.IsSuccess = false;
                _response.Message = errorMessage;
                return BadRequest();
            }

            return Ok(_response);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }
    }
}
