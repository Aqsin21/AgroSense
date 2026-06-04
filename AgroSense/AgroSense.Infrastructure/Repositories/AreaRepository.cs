using AgroSense.Application.Interfaces.Repositories;
using AgroSense.Domain.Entities;
using AgroSense.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;


namespace AgroSense.Infrastructure.Repositories
{
    public class AreaRepository : IAreaRepository
    {
        private readonly AppDbContext _context;

        public AreaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Area>> GetAllAsync()
        {
            return await _context.Areas
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<List<Area>> GetByOwnerUserIdAsync(string ownerUserId)
        {
            return await _context.Areas
                .AsNoTracking()
                .Where(x => x.OwnerUserId == ownerUserId)
                .ToListAsync();
        }

        public async Task<Area?> GetByIdAsync(int id)
        {
            return await _context.Areas
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Area?> GetByIdAndOwnerUserIdAsync(int id, string ownerUserId)
        {
            return await _context.Areas
                .FirstOrDefaultAsync(x => x.Id == id && x.OwnerUserId == ownerUserId);
        }

        public async Task AddAsync(Area area)
        {
            await _context.Areas.AddAsync(area);
        }

        public void Update(Area area)
        {
            _context.Areas.Update(area);
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
