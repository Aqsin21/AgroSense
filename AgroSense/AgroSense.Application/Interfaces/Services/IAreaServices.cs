using AgroSense.Application.Dtos.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroSense.Application.Interfaces.Services
{
    public interface IAreaService
    {
        Task<List<AreaResponse>> GetAreasAsync(string userId, bool isAdmin);

        Task<AreaResponse?> GetAreaByIdAsync(int id, string userId, bool isAdmin);

        Task<AreaResponse> CreateAreaAsync(CreateAreaRequest request, string ownerUserId);

        Task<AreaResponse?> UpdateAreaAsync(int id, UpdateAreaRequest request, string userId, bool isAdmin);

        Task<bool> UpdateAreaStatusAsync(int id, bool isActive, string userId, bool isAdmin);
    }
}
