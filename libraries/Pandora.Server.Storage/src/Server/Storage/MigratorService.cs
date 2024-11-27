using FluentMigrator.Runner;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pandora.Server.Storage;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.Storage
{
    public interface IMigratorService
    {
        void Migrate();
    }

    public class MigratorService : IMigratorService
    {
        private IEnumerable<MigrationExecutionContainer> list;

        public MigratorService(IEnumerable<MigrationExecutionContainer> list)
        {
            this.list = list;
        }

        public void Migrate()
        {
            foreach (var item in list)
            {
                item.ExecuteMigration();
            }
        }
    }

    public static class MigratorServiceExtensions
    {
        public static IServiceCollection AddMigratorService(this IServiceCollection services, Action<MigratorServiceBuilder> action)
        {
            services.AddSingleton<IMigratorService>(sp =>
            {
                var list = new HashSet<MigrationExecutionContainer>();
                var builder = new MigratorServiceBuilder(list);

                return new MigratorService(list);
            });

            return services;
        }

        public static IApplicationBuilder UseMigration(this IApplicationBuilder applicationBuilder)
        {
            var migratorService = applicationBuilder.ApplicationServices.GetRequiredService<IMigratorService>();

            migratorService.Migrate();

            return applicationBuilder;
        }
    }

    public class MigratorServiceBuilder
    {
        private ICollection<MigrationExecutionContainer> list;

        public MigratorServiceBuilder(ICollection<MigrationExecutionContainer> list)
        {
            this.list = list;
        }

        public MigratorServiceBuilder AddMigration<T>(string connectionString)
               where T : IMigratorExecutor, new()
        {
            var container = new MigrationExecutionContainer(new T(),connectionString);

            container.InitializeServiceProvider();

            list.Add(container);

            return this;
        }
    }

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