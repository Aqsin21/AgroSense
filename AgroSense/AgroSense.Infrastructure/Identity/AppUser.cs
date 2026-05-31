using Microsoft.AspNetCore.Identity;
namespace AgroSense.Infrastructure.Identity
{
    public class AppUser : IdentityUser
    {
        public string FullName { get; set; } = null!;

        public bool IsActive { get; set; } = true;

        public bool MustChangePassword { get; set; } = true;
    }
}
