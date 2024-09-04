using Microsoft.AspNetCore.Identity;

namespace Online.Shopping.AuthApi.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
