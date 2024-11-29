using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;

namespace Pandora.Server.Storage
{
    public static class MigratorServiceExtensions
    {
        public static IServiceCollection AddMigratorService(this IServiceCollection services, Action<MigratorServiceBuilder> action)
        {
            services.AddSingleton<IMigratorService>(sp =>
            {
                var list = new HashSet<MigrationExecutionContainer>();
                var builder = new MigratorServiceBuilder(list);

                action?.Invoke(builder);

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
}