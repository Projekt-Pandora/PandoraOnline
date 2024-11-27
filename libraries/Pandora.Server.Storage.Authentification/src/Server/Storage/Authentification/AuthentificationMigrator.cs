using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Pandora.Server.Storage.Authentification.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.Storage.Authentification
{
    public class AuthentificationMigrator : IMigratorExecutor
    {
        public void ConfigureServices(ServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection
                // Add common FluentMigrator services
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    // Add SQLite support to FluentMigrator
                    .AddPostgres15_0()
                    // Set the connection string
                    .WithGlobalConnectionString(connectionString)
                    // Define the assembly containing the migrations
                    .ScanIn(typeof(_27112024_01).Assembly).For.Migrations())
                // Enable logging to console in the FluentMigrator way
                .AddLogging(lb => lb.AddFluentMigratorConsole());
        }
    }
}
