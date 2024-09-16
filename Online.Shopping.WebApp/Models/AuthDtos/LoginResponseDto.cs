using OOnline.Shopping.WebApp.Models.AuthDtos;

namespace Online.Shopping.WebApp.Models.AuthDtos
{
    public class LoginResponseDto
    {
        public UserDto User { get; set; }
        public string Token { get; set; }
    }
}
