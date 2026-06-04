using AgroSense.Application.Dtos.Areas;
using AgroSense.Application.Interfaces.Repositories;
using AgroSense.Application.Interfaces.Services;
using AgroSense.Domain.Entities;
namespace AgroSense.Application.Services
{
    public class AreaService : IAreaService
    {
        private readonly IAreaRepository _areaRepository;

        public AreaService(IAreaRepository areaRepository)
        {
            _areaRepository = areaRepository;
        }

        public async Task<List<AreaResponse>> GetAreasAsync(string userId, bool isAdmin)
        {
            var areas = isAdmin
                ? await _areaRepository.GetAllAsync()
                : await _areaRepository.GetByOwnerUserIdAsync(userId);

            return areas.Select(MapToResponse).ToList();
        }

        public async Task<AreaResponse?> GetAreaByIdAsync(int id, string userId, bool isAdmin)
        {
            var area = isAdmin
                ? await _areaRepository.GetByIdAsync(id)
                : await _areaRepository.GetByIdAndOwnerUserIdAsync(id, userId);

            return area is null ? null : MapToResponse(area);
        }

        public async Task<AreaResponse> CreateAreaAsync(CreateAreaRequest request, string ownerUserId)
        {
            var area = new Area
            {
                Name = request.Name,
                AreaType = request.AreaType,
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                OwnerUserId = ownerUserId,
                IsActive = true
            };

            await _areaRepository.AddAsync(area);
            await _areaRepository.SaveChangesAsync();

            return MapToResponse(area);
        }

        public async Task<AreaResponse?> UpdateAreaAsync(
            int id,
            UpdateAreaRequest request,
            string userId,
            bool isAdmin)
        {
            var area = isAdmin
                ? await _areaRepository.GetByIdAsync(id)
                : await _areaRepository.GetByIdAndOwnerUserIdAsync(id, userId);

            if (area is null)
            {
                return null;
            }

            area.Name = request.Name;
            area.AreaType = request.AreaType;
            area.Latitude = request.Latitude;
            area.Longitude = request.Longitude;

            _areaRepository.Update(area);
            await _areaRepository.SaveChangesAsync();

            return MapToResponse(area);
        }

        public async Task<bool> UpdateAreaStatusAsync(
            int id,
            bool isActive,
            string userId,
            bool isAdmin)
        {
            var area = isAdmin
                ? await _areaRepository.GetByIdAsync(id)
                : await _areaRepository.GetByIdAndOwnerUserIdAsync(id, userId);

            if (area is null)
            {
                return false;
            }

            area.IsActive = isActive;

            _areaRepository.Update(area);
            await _areaRepository.SaveChangesAsync();

            return true;
        }

        private static AreaResponse MapToResponse(Area area)
        {
            return new AreaResponse
            {
                Id = area.Id,
                Name = area.Name,
                AreaType = area.AreaType,
                Latitude = area.Latitude,
                Longitude = area.Longitude,
                IsActive = area.IsActive,
                OwnerUserId = area.OwnerUserId
            };
        }
    }
}
