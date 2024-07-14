using Microsoft.AspNetCore.Identity;

namespace Jungle.Data
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public bool IsTenant { get; set; }
    }
}
