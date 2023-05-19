using Microsoft.AspNetCore.Identity;

namespace Domain.Security
{
    public class AppIdentityRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
