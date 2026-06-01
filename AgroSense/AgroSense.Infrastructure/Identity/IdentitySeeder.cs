using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
namespace AgroSense.Infrastructure.Identity
{
    public static class IdentitySeeder
    {
        public static async Task SeedAsync(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            await SeedRolesAsync(roleManager);
            await SeedAdminUserAsync(userManager, configuration);

        }
        private static async Task SeedRolesAsync(RoleManager<IdentityRole> roleManager)
        {
            foreach (var role in IdentityRoles.All)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }
        }
        private static async Task SeedAdminUserAsync(
            UserManager<AppUser> userManager,
            IConfiguration configuration)
        {
            var adminEmail = configuration["AdminUser:Email"];
            var adminPassword = configuration["AdminUser:Password"];
            var adminFullName = configuration["AdminUser:FullName"] ?? "System Admin";

            if (string.IsNullOrWhiteSpace(adminEmail) ||
                string.IsNullOrWhiteSpace(adminPassword))
            {
                throw new InvalidOperationException("Admin user credentials are not configured.");
            }

            var existingAdmin = await userManager.FindByEmailAsync(adminEmail);

            if (existingAdmin is not null)
            {
                return;
            }

            var adminUser = new AppUser
            {
                UserName = adminEmail,
                Email = adminEmail,
                FullName = adminFullName,
                EmailConfirmed = true,
                IsActive = true,
                MustChangePassword = true
            };

            var createResult = await userManager.CreateAsync(adminUser, adminPassword);

            if (!createResult.Succeeded)
            {
                var errors = string.Join(", ", createResult.Errors.Select(x => x.Description));
                throw new InvalidOperationException($"Failed to create admin user: {errors}");
            }

            var roleResult = await userManager.AddToRoleAsync(adminUser, IdentityRoles.Admin);

            if (!roleResult.Succeeded)
            {
                var errors = string.Join(", ", roleResult.Errors.Select(x => x.Description));
                throw new InvalidOperationException($"Failed to assign admin role: {errors}");
            }
        }
    }
}
