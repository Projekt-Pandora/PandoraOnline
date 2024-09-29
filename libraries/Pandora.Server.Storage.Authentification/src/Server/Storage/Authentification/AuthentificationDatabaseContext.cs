using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.Storage.Authentification
{
    public class AuthentificationDatabaseContext : DbContext
    {
        private readonly IMigrationRunner migrationRunner;
        private bool isMigrated;

        public AuthentificationDatabaseContext(DbContextOptions<AuthentificationDatabaseContext> options, IMigrationRunner migrationRunner) : base(options)
        {
            this.migrationRunner = migrationRunner;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(isMigrated) return;

            migrationRunner.MigrateUp();
        }
    }
}
