using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Online.Shopping.CouponsApi.Data;
using Online.Shopping.CouponsApi.Models;
using Online.Shopping.CouponsApi.Models.Dtos;

namespace Online.Shopping.CouponsApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private ResponseDto _response;
        private IMapper _mapper;
        public CouponsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _response = new ResponseDto();
        }

        [HttpGet]
        public ResponseDto Get()
        {
            try
            {
                IEnumerable<Coupon> coupons = _context.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDto>>(coupons);

            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet("{id}")]
        public ResponseDto Get(int id)
        {
            try
            {
                Coupon coupon = _context.Coupons.Where(x => x.CouponId == id).FirstOrDefault()!;
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpGet]
        [Route("GetByCouponCode/{code}")]
        public ResponseDto GetByCouponCode(string code)
        {
            try
            {
                Coupon coupon = _context.Coupons.Where(x => x.CouponCode.ToLower() == code.ToLower()).FirstOrDefault()!;

                if(coupon == null)
                {
                    _response.IsSuccess = false;
                }
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPost]
        public ResponseDto CreateCoupon([FromBody] CouponDto couponDto)
        {
            try
            {
                // Map Dto to entity
                var coupon = _mapper.Map<Coupon>(couponDto);

                _context.Coupons.Add(coupon);

                _context.SaveChanges();

                // Map the entity back to Dto
                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch(Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpPut]
        public ResponseDto UpdateCoupon([FromBody] CouponDto couponDto)
        {
            try
            {
                var coupon = _mapper.Map<Coupon>(couponDto);

                _context.Coupons.Update(coupon);

                _context.SaveChanges();

                _response.Result = _mapper.Map<CouponDto>(coupon);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        public ResponseDto DeleteCoupon(int id)
        {
            try
            {
                var coupon = _context.Coupons.Where(x => x.CouponId == id).FirstOrDefault()!;
                _context.Coupons.Remove(coupon);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.Message = ex.Message;
            }
            return _response;
        }
    }
}
