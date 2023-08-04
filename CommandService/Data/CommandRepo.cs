using CommandService.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandService.Data
{
    public class CommandRepo : ICommandRepo
    {
        private readonly AppDbContext _context;

        public CommandRepo(AppDbContext appDbContext)
        {
            _context = appDbContext;
        }

        public async Task CreateCommand(int platformId, Command command)
        {
            if (command is null)
            {
                throw new ArgumentNullException(nameof(command));
            }

            command.PlatformId = platformId;
            await _context
                .Commands
                .AddAsync(command);
        }

        public async Task CreatePlatform(Platform platform)
        {
            if (platform is null)
            {
                throw new ArgumentNullException(nameof(platform));
            }

            await _context
                .Platforms
                .AddAsync(platform);
        }

        public async Task<bool> ExternalPlatformExists(int externalPlatformId) =>
                await _context
                .Platforms
                .AnyAsync(p => p.ExternalId == externalPlatformId);

        public async Task<IEnumerable<Platform>> GetAllPlatforms() => await _context.Platforms.ToListAsync();

        public async Task<Command> GetCommand(int platformId, int commandId) =>
            await _context.
                Commands
                .Where(c => c.PlatformId == platformId && c.Id == commandId)
                .FirstOrDefaultAsync();

        public async Task<IEnumerable<Command>> GetCommandsForPlatform(int platformId) => await _context
                .Commands
                .Where(c => c.PlatformId == platformId)
                .OrderBy(c => c.Platform.Name)
                .ToListAsync();

        public async Task<bool> PlatformExist(int platformId) => await _context
                .Platforms
                .AnyAsync(p => p.Id == platformId);

        public async Task<bool> SaveChanges() => await _context
                .SaveChangesAsync() >= 0;
    }
}