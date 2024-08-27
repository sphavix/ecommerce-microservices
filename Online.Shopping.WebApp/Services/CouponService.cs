using Online.Shopping.WebApp.Models;
using Online.Shopping.WebApp.Services.Contracts;
using Online.Shopping.WebApp.Utilities;

namespace Online.Shopping.WebApp.Services
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService(IBaseService baseService)
        {
            _baseService = baseService ?? throw new ArgumentNullException(nameof(baseService));
        }

        public async Task<ResponseDto?> GetCouponsAsync()
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ServiceDependency.ApiType.GET,
                ApiUrl = ServiceDependency.CouponApiBaseUrl + "/api/coupons"
            });
        }

        public async Task<ResponseDto?> GetCouponAsync(int couponId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ServiceDependency.ApiType.GET,
                ApiUrl = ServiceDependency.CouponApiBaseUrl + "/api/coupons/" + couponId
            });
        }

        public async Task<ResponseDto?> GetCouponAsync(string couponCode)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ServiceDependency.ApiType.GET,
                ApiUrl = ServiceDependency.CouponApiBaseUrl + "/api/coupons/GetByCouponCode/" + couponCode
            });
        }

        public async Task<ResponseDto?> CreateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto() 
            { 
                ApiType = ServiceDependency.ApiType.POST,
                Data = couponDto, // Dto is serialized here in the BaseService
                ApiUrl = ServiceDependency.CouponApiBaseUrl + "/api/coupons" 
            });
        }
        public async Task<ResponseDto?> UpdateCouponAsync(CouponDto couponDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ServiceDependency.ApiType.PUT,
                Data = couponDto,
                ApiUrl = ServiceDependency.CouponApiBaseUrl + "/api/coupons"
            });
        }

        public async Task<ResponseDto?> DeleteCouponAsync(int id)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = ServiceDependency.ApiType.DELETE,
                ApiUrl = ServiceDependency.CouponApiBaseUrl + "/api/coupons/" + id
            });
        }

    }
}
