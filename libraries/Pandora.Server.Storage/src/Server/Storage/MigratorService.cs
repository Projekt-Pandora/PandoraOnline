using Pandora.Server.Storage;
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
}