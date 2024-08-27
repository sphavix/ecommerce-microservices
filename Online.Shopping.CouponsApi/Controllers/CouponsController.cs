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
    }
}
