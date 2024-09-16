using Online.Shopping.AuthApi.Models;

namespace Online.Shopping.AuthApi.Services.Contracts
{
    public interface IGenerateJwtToken
    {
        string GenerateToken(ApplicationUser applicationUser);
    }
}
