using Online.Shopping.WebApp.Models;

namespace Online.Shopping.WebApp.Services.Contracts
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
