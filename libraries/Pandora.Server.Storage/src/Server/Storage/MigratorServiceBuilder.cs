using System.Collections.Generic;

namespace Pandora.Server.Storage
{
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
}