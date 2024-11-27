using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.Storage
{
    public interface IMigratorExecutor
    {
        void ConfigureServices(ServiceCollection serviceCollection, string connectionString);
    }
}
