using static Online.Shopping.WebApp.Utilities.ServiceDependency;

namespace Online.Shopping.WebApp.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string ApiUrl { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
