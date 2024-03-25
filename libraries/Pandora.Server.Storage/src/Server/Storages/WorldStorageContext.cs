using EvolveDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using Pandora.Server.Storages.Authentification;
using System;
using System.IO;
using System.Reflection;

namespace Pandora.Server.Storages.World
{
    public sealed class WorldStorageContext : DbContext, IStorageMigrator, IWorldStorageContext
    {
        private static bool __isMigrated;
        private readonly ILogger<AuthentificationStorageContext> logger;
        private readonly IHostEnvironment hostEnvironment;

        public WorldStorageContext(DbContextOptions<WorldStorageContext> options, ILogger<AuthentificationStorageContext> logger, IHostEnvironment hostEnvironment) : base(options)
        {
            this.logger = logger;
            this.hostEnvironment = hostEnvironment;
        }

        public void Migrate(string? connectionString)
        {
            if (!__isMigrated)
            {
                try
                {
                    var env = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                    var cnx = new NpgsqlConnection(connectionString);
                    var evolve = new Evolve(cnx, msg => logger.LogInformation(msg))
                    {
                        Locations = new[] { Path.Combine(env, "Migration", "Authentification") },
                        IsEraseDisabled = true,
                        MustEraseOnValidationError = hostEnvironment.IsDevelopment(),
                    };

                    evolve.Migrate();
                    __isMigrated = true; ;
                }
                catch (Exception ex)
                {
                    logger.LogCritical(ex, "Authentification database migration failed.");
                    throw;
                }
            }
        }
    }
}
