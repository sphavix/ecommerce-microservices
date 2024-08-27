using Microsoft.EntityFrameworkCore;
using Online.Shopping.CouponsApi.Models;

namespace Online.Shopping.CouponsApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Coupon> Coupons { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Coupon>().HasData(
                new Coupon
                {
                    CouponId = 1,
                    CouponCode = "10OFF",
                    DiscountAmount = 10,
                    MinimumAmount = 20
                },
                new Coupon
                {
                    CouponId = 2,
                    CouponCode = "20OFF",
                    DiscountAmount = 20,
                    MinimumAmount = 40
                });
        }
    }
}
