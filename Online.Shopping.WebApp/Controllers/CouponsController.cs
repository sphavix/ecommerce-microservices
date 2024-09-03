using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Online.Shopping.WebApp.Models;
using Online.Shopping.WebApp.Services.Contracts;

namespace Online.Shopping.WebApp.Controllers
{
    public class CouponsController : Controller
    {
        private readonly ICouponService _couponService;

        public CouponsController(ICouponService couponService)
        {
            _couponService = couponService ?? throw new ArgumentNullException(nameof(couponService));
        }

        public async Task<IActionResult> CouponsList()
        {
            List<CouponDto?> coupons = new();

            ResponseDto? response = await _couponService.GetCouponsAsync();

            if(response != null && response.IsSuccess)
            {
                coupons = JsonConvert.DeserializeObject<List<CouponDto?>>(Convert.ToString(response.Result))!;
            }
            return View(coupons);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCoupon()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCoupon(CouponDto coupon)
        {
            if(ModelState.IsValid)
            {
                ResponseDto? response = await _couponService.CreateCouponAsync(coupon);

                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(CouponsList));
                }
            }
            return View(coupon);
        }
    }
}
