using Microsoft.EntityFrameworkCore;
using PlatformService.Models;

namespace PlatformService.Data
{
    public class PlatformRepo : IPlatformRepo
    {
        private readonly AppDbContext _context;

        public PlatformRepo(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreatePlatform(Platform platform)
        {
            if(platform == null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            await _context.AddAsync(platform);
        }

        public async Task<IEnumerable<Platform>> GetAllPlatforms() => await _context.Platforms.ToListAsync();

        public async Task<Platform?> GetPlatformById(int id) => await _context.Platforms.FirstOrDefaultAsync(m => m.Id == id);

        public async Task<bool> SaveChanges() => await _context.SaveChangesAsync() >= 0;
    }
}
