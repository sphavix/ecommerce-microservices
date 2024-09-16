namespace Online.Shopping.WebApp.Utilities
{
    public class ServiceDependency
    {
        public static string CouponApiBaseUrl { get; set; }
        public static string AuthApiBaseUrl {  get; set; }
        public const string RoleAdmin = "ADMIN";
        public const string RoleCustomer = "CUSTOMER";
        public enum ApiType
        {
            GET,
            POST,
            PUT,
            DELETE
        }
    }
}
