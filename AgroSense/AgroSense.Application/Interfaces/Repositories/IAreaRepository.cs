using AgroSense.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroSense.Application.Interfaces.Repositories
{
    public interface IAreaRepository
    {
        Task<List<Area>> GetAllAsync();

        Task<List<Area>> GetByOwnerUserIdAsync(string ownerUserId);

        Task<Area?> GetByIdAsync(int id);

        Task<Area?> GetByIdAndOwnerUserIdAsync(int id, string ownerUserId);

        Task AddAsync(Area area);

        void Update(Area area);

        Task<int> SaveChangesAsync();
    }
}
