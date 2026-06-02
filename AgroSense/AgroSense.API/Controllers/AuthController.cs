using AgroSense.Application.Dtos.Auth;
using AgroSense.Application.Interfaces;
using AgroSense.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
namespace AgroSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthController(
            UserManager<AppUser> userManager,
            IJwtTokenService jwtTokenService)
        {
            _userManager = userManager;
            _jwtTokenService = jwtTokenService;
        }
        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user is null)
            {
                return Unauthorized("Email or password is incorrect.");
            }

            if (!user.IsActive)
            {
                return Unauthorized("User account is inactive.");
            }

            var isPasswordValid = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!isPasswordValid)
            {
                return Unauthorized("Email or password is incorrect.");
            }

            var roles = await _userManager.GetRolesAsync(user);

            var token = _jwtTokenService.GenerateToken(
                user.Id,
                user.Email!,
                user.FullName,
                roles);

            var response = new LoginResponse
            {
                Token = token,
                Email = user.Email!,
                FullName = user.FullName,
                Roles = roles,
                MustChangePassword = user.MustChangePassword
            };

            return Ok(response);
        }

        [Authorize]
        [HttpPost("Change Password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrWhiteSpace(userId))
            {
                return Unauthorized();
            }

            var user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return Unauthorized();
            }

            var result = await _userManager.ChangePasswordAsync(
                user,
                request.CurrentPassword,
                request.NewPassword);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors.Select(x => x.Description));
            }

            user.MustChangePassword = false;

            var updateResult = await _userManager.UpdateAsync(user);

            if (!updateResult.Succeeded)
            {
                return BadRequest(updateResult.Errors.Select(x => x.Description));
            }

            return Ok("Password changed successfully.");
        }

    }
}
