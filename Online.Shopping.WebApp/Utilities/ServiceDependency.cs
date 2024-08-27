namespace Online.Shopping.WebApp.Utilities
{
    public class ServiceDependency
    {
        public static string CouponApiBaseUrl { get; set; }
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
