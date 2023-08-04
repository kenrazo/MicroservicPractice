using CommandService.Models;
using CommandService.SyncDataServices.Grpc;

namespace CommandService.Data
{
    public static class PrepDb
    {
        public static async Task PrepPopulation(IApplicationBuilder applicationBuilder)
        {
            using var serviceScope = applicationBuilder.ApplicationServices.CreateScope();
            var grpcClient = serviceScope.ServiceProvider.GetService<IPlatformDataClient>();

            var platforms = grpcClient.ReturnAllPlatforms();

            SeedData(serviceScope.ServiceProvider.GetService<ICommandRepo>(), platforms);
        }

        private static async Task SeedData(ICommandRepo repository, IEnumerable<Platform> platforms)
        {
            Console.WriteLine($"--> Seeding new platforms...");
            foreach (var platform in platforms)
            {
                if (!await repository.ExternalPlatformExists(platform.ExternalId))
                {
                    await repository.CreatePlatform(platform);
                }
                await repository.SaveChanges();
            }
        }
    }
}