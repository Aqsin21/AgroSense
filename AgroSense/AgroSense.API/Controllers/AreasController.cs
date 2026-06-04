using AgroSense.Application.Dtos.Areas;
using AgroSense.Application.Interfaces.Services;
using AgroSense.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AgroSense.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AreasController : ControllerBase
    {
        private readonly IAreaService _areaService;

        public AreasController(IAreaService areaService)
        {
            _areaService = areaService;
        }

        [HttpGet]
        [Authorize(Roles = $"{IdentityRoles.Admin},{IdentityRoles.Operator},{IdentityRoles.Viewer}")]
        public async Task<ActionResult<List<AreaResponse>>> GetAreas()
        {
            var userId = GetCurrentUserId();

            if (userId is null)
            {
                return Unauthorized();
            }

            var isAdmin = User.IsInRole(IdentityRoles.Admin);

            var areas = await _areaService.GetAreasAsync(userId, isAdmin);

            return Ok(areas);
        }

        [HttpGet("{id}")]
        [Authorize(Roles = $"{IdentityRoles.Admin},{IdentityRoles.Operator},{IdentityRoles.Viewer}")]
        public async Task<ActionResult<AreaResponse>> GetAreaById(int id)
        {
            var userId = GetCurrentUserId();

            if (userId is null)
            {
                return Unauthorized();
            }

            var isAdmin = User.IsInRole(IdentityRoles.Admin);

            var area = await _areaService.GetAreaByIdAsync(id, userId, isAdmin);

            if (area is null)
            {
                return NotFound("Area not found.");
            }

            return Ok(area);
        }

        [HttpPost]
        [Authorize(Roles = $"{IdentityRoles.Admin},{IdentityRoles.Operator}")]
        public async Task<ActionResult<AreaResponse>> CreateArea(CreateAreaRequest request)
        {
            var userId = GetCurrentUserId();

            if (userId is null)
            {
                return Unauthorized();
            }

            var area = await _areaService.CreateAreaAsync(request, userId);

            return Ok(area);
        }

        [HttpPut("{id}")]
        [Authorize(Roles = $"{IdentityRoles.Admin},{IdentityRoles.Operator}")]
        public async Task<ActionResult<AreaResponse>> UpdateArea(
            int id,
            UpdateAreaRequest request)
        {
            var userId = GetCurrentUserId();

            if (userId is null)
            {
                return Unauthorized();
            }

            var isAdmin = User.IsInRole(IdentityRoles.Admin);

            var area = await _areaService.UpdateAreaAsync(id, request, userId, isAdmin);

            if (area is null)
            {
                return NotFound("Area not found.");
            }

            return Ok(area);
        }

        [HttpPatch("{id}/status")]
        [Authorize(Roles = $"{IdentityRoles.Admin},{IdentityRoles.Operator}")]
        public async Task<IActionResult> UpdateAreaStatus(
            int id,
            UpdateAreaStatusRequest request)
        {
            var userId = GetCurrentUserId();

            if (userId is null)
            {
                return Unauthorized();
            }

            var isAdmin = User.IsInRole(IdentityRoles.Admin);

            var updated = await _areaService.UpdateAreaStatusAsync(
                id,
                request.IsActive,
                userId,
                isAdmin);

            if (!updated)
            {
                return NotFound("Area not found.");
            }

            return Ok("Area status updated successfully.");
        }

        private string? GetCurrentUserId()
        {
            return User.FindFirstValue(ClaimTypes.NameIdentifier);
        }
    }
}
