using Online.Shopping.WebApp.Models;

namespace Online.Shopping.WebApp.Services.Contracts
{
    public interface ICouponService
    {
        Task<ResponseDto?> GetCouponsAsync();
        Task<ResponseDto?> GetCouponAsync(int couponId);
        Task<ResponseDto?> GetCouponAsync(string couponCode);
        Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto);
        Task<ResponseDto?> DeleteCouponAsync(int id);
    }
}
