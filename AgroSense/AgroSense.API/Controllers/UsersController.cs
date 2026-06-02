using AgroSense.Application.Dtos.Users;
using AgroSense.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AgroSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = IdentityRoles.Admin)]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpPost]
        public async Task<ActionResult<UserResponse>> CreateUser(CreateUserRequest request)
        {
            var roleExists = await _roleManager.RoleExistsAsync(request.Role);

            if (!roleExists)
            {
                return BadRequest($"Role '{request.Role}' does not exist.");
            }

            var existingUser = await _userManager.FindByEmailAsync(request.Email);

            if (existingUser is not null)
            {
                return BadRequest("A user with this email already exists.");
            }

            var user = new AppUser
            {
                UserName = request.Email,
                Email = request.Email,
                FullName = request.FullName,
                EmailConfirmed = true,
                IsActive = true,
                MustChangePassword = true
            };

            var createResult = await _userManager.CreateAsync(user, request.TemporaryPassword);

            if (!createResult.Succeeded)
            {
                return BadRequest(createResult.Errors.Select(x => x.Description));
            }

            var roleResult = await _userManager.AddToRoleAsync(user, request.Role);

            if (!roleResult.Succeeded)
            {
                return BadRequest(roleResult.Errors.Select(x => x.Description));
            }

            var roles = await _userManager.GetRolesAsync(user);

            var response = new UserResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                IsActive = user.IsActive,
                MustChangePassword = user.MustChangePassword,
                Roles = roles
            };

            return Ok(response);
        }
    }
}
