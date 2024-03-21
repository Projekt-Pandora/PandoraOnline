namespace Pandora.Server
{
    public interface IStorageMigrator
    {
        void Migrate(string? connectionString);
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
