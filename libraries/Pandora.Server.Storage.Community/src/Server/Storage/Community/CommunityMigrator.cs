using FluentMigrator.Runner;
using Microsoft.Extensions.DependencyInjection;
using Pandora.Server.Storage.Community.Migrations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.Storage.Community
{
    public class CommunityMigrator : IMigratorExecutor
    {
        public void ConfigureServices(ServiceCollection serviceCollection, string connectionString)
        {
            serviceCollection
                .AddFluentMigratorCore()
                .ConfigureRunner(rb => rb
                    .AddPostgres15_0()
                    .WithGlobalConnectionString(connectionString)
                    .ScanIn(typeof(M_0001).Assembly).For.Migrations())
                .AddLogging(lb => lb.AddFluentMigratorConsole());
        }
    }
}
