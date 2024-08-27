using AutoMapper;
using Online.Shopping.CouponsApi.Models;
using Online.Shopping.CouponsApi.Models.Dtos;

namespace Online.Shopping.CouponsApi.Configurations
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<CouponDto, Coupon>().ReverseMap();
        }

    }
}
