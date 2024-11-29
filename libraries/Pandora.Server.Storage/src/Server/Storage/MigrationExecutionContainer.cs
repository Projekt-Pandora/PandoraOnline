using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;

namespace Pandora.Server.Storage
{
    public class MigrationExecutionContainer(IMigratorExecutor migrator, string connectionString)
    {
        private ServiceProvider serviceProvider = null!;

        public override int GetHashCode()
        {
            return migrator.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            return migrator.Equals(obj);
        }

        public void InitializeServiceProvider()
        {
            var serviceCollection = new ServiceCollection();

            migrator.ConfigureServices(serviceCollection, connectionString);

            serviceProvider = serviceCollection.BuildServiceProvider();
        }

        public void ExecuteMigration()
        {
            var migrationRunner = serviceProvider.GetRequiredService<IMigrationRunner>();

            migrationRunner.MigrateUp();
        }
    }
}