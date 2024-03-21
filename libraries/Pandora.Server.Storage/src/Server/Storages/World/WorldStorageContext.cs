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

    //public sealed class StorageContextFactory : IDbContextFactory<IStorageContext>
    //{
    //    private readonly ILogger<StorageContextFactory> logger;
    //    private readonly IConfiguration configuration;

    //    public StorageContextFactory(ILogger<StorageContextFactory> logger, IConfiguration configuration)
    //    {
    //        this.logger = logger;
    //        this.configuration = configuration;
    //    }

    //    public IStorageContext Create<T>()
    //        where T : IStorageContext
    //    {
    //        IStorageContext result;

    //        switch (typeof(T))
    //        {
    //            case var impl when impl == typeof(AuthentificationStorageContext):
    //                {
    //                    var context = BuildStorageContext<AuthentificationStorageContext>("AuthentificationDatabase");
    //                    context.Migrate(logger);

    //                    return context;
    //                }

    //            case var impl when impl == typeof(WorldStorageContext):
    //                {
    //                    var context = BuildStorageContext<AuthentificationStorageContext>("WorldDatabase");
    //                    context.Migrate(logger);

    //                    return context;
    //                }

    //            case var impl when impl == typeof(EventStorageContext):
    //                {
    //                    var context = BuildStorageContext<AuthentificationStorageContext>("EventsDatabase");
    //                    context.Migrate(logger);

    //                    return context;
    //                }

    //            default:
    //                throw new ArgumentException($"Type '{typeof(T).FullName}' not supported");
    //        }
    //    }

    //    public IStorageContext CreateDbContext()
    //    {
    //        throw new NotImplementedException();
    //    }

    //    private T BuildStorageContext<T>(string connectionName)
    //        where T : IStorageContext
    //    {
    //        var connectionString = ReadConnectionString(connectionName);
    //        var instance = Activator.CreateInstance(typeof(T), new object[] { connectionString }) ?? throw new InvalidOperationException($"Type '{typeof(T)}' could not be activated.");

    //        return (T)instance;
    //    }

    //    private string ReadConnectionString(string connectionName)
    //    {
    //        return configuration.GetConnectionString(connectionName) ?? throw new InvalidOperationException($"Connectionstring for '{connectionName}' not found");
    //    }
    //}
}
