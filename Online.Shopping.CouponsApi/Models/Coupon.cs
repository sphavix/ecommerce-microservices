namespace Online.Shopping.CouponsApi.Models
{
    public class Coupon
    {
        public int CouponId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public double DiscountAmount { get; set; }
        public int MinimumAmount { get; set; }
    }
}
