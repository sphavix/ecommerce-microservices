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
            else
            {
                TempData["error"] = response?.Message;
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
                    TempData["success"] = "Coupon has been created successfully!";
                    return RedirectToAction(nameof(CouponsList));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(coupon);
        }


        public async Task<IActionResult> DeleteCoupon(int couponId)
        {
            ResponseDto? response = await _couponService.GetCouponAsync(couponId);

            if (response != null && response.IsSuccess)
            {
                CouponDto? coupon = JsonConvert.DeserializeObject<CouponDto?>(Convert.ToString(response.Result))!;
                return View(coupon);
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]        
        public async Task<IActionResult> DeleteCoupon(CouponDto couponDto)
        {
            ResponseDto? response = await _couponService.DeleteCouponAsync(couponDto.CouponId);

            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Coupon has been deleted successfully!";
                return RedirectToAction(nameof(CouponsList));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(couponDto);
        }
    }
}
