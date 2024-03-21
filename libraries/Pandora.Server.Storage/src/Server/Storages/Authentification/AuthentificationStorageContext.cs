using EvolveDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Npgsql;
using System;
using System.IO;
using System.Reflection;

namespace Pandora.Server.Storages.Authentification
{
    public sealed class AuthentificationStorageContext : DbContext, IStorageMigrator, IAuthentificationStorageContext
    {
        private static bool __isMigrated;
        private readonly ILogger<AuthentificationStorageContext> logger;
        private readonly IHostEnvironment hostEnvironment;

        public AuthentificationStorageContext(DbContextOptions<AuthentificationStorageContext> options, ILogger<AuthentificationStorageContext> logger, IHostEnvironment hostEnvironment) : base(options)
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

        public DbSet<Account> Accounts { get; private set; }
    }
}
