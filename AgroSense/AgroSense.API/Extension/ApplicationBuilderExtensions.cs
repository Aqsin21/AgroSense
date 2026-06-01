using AgroSense.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;

namespace AgroSense.API.Extension
{
    public static class ApplicationBuilderExtensions
    {
        public static async Task<WebApplication> SeedIdentityAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();

            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var configuration = scope.ServiceProvider.GetRequiredService<IConfiguration>();

            await IdentitySeeder.SeedAsync(userManager, roleManager, configuration);

            return app;
        }
    }
}
