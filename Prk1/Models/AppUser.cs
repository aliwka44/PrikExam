using Microsoft.AspNetCore.Identity;

namespace Prk1.Models
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
