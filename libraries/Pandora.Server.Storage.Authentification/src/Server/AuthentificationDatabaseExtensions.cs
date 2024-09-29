using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pandora.Server.Storage.Authentification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server
{
    public static class AuthentificationDatabaseExtensions
    {
        public static IServiceCollection AddAuthentificationDatabaseContext(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextFactory<AuthentificationDatabaseContext>(options => options.UseNpgsql(connectionString))
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres15_0()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(AuthentificationDatabaseExtensions).Assembly).For.Migrations()
                    )
                ;


            return services;
        }
    }
}
