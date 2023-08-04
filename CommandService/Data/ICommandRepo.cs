using CommandService.Models;

namespace CommandService.Data
{
    public interface ICommandRepo
    {
        Task<bool> SaveChanges();

        //Platform
        Task<IEnumerable<Platform>> GetAllPlatforms();
        Task CreatePlatform(Platform platform);
        Task<bool> PlatformExist(int platformId);
        Task<bool> ExternalPlatformExists(int externalPlatformId);

        //Command
        Task<IEnumerable<Command>> GetCommandsForPlatform(int platformId);
        Task<Command> GetCommand(int platformId, int commandId);
        Task CreateCommand(int platformId, Command command);

    }
}