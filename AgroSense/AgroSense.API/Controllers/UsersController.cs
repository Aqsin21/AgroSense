using AgroSense.Application.Dtos.Users;
using AgroSense.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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

        [HttpGet]
        public async Task<ActionResult<List<UserResponse>>> GetUsers()
        {
            var users= await _userManager.Users.ToListAsync();
            var response= new List<UserResponse>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                response.Add(new UserResponse
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    IsActive = user.IsActive,
                    MustChangePassword = user.MustChangePassword,
                    Roles = roles
                }
                );
               

            }
            return Ok(response);
        }

        [HttpPatch("{id}/status")]
        public async Task<IActionResult> UpdateUserStatus(string id, UpdateUserStatusRequest request)    
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
                return NotFound("User not found");
            user.IsActive=request.IsActive;
            var result =await _userManager.UpdateAsync(user);
            if(!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description));
            }

            return Ok("User status updated successfully.");
        }

        [HttpPut("{id}/role")]
        public async Task<IActionResult> UpdateUserRole(string id, UpdateUserRoleRequest request)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return NotFound("User not found");

            var roleExists = await _roleManager.RoleExistsAsync(request.Role);
            if (!roleExists)
            {
                return NotFound($"Role '{request.Role}' does not exist");
            }
            var currentRoles = await _userManager.GetRolesAsync(user);

            var removeResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);

            if (!removeResult.Succeeded)
            {
                return BadRequest(removeResult.Errors.Select(x => x.Description));
            }

            var addResult = await _userManager.AddToRoleAsync(user, request.Role);

            if (!addResult.Succeeded)
            {
                return BadRequest(addResult.Errors.Select(x => x.Description));
            }

            var roles = await _userManager.GetRolesAsync(user);

            return Ok(new UserResponse
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                IsActive = user.IsActive,
                MustChangePassword = user.MustChangePassword,
                Roles = roles
            });
        }
    }
}
