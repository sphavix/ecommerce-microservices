namespace Online.Shopping.CouponsApi.Models.Dtos
{
    public class CouponDto
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public double DiscountAmount { get; set; }
        public int MinimumAmount { get; set; }
    }
}
