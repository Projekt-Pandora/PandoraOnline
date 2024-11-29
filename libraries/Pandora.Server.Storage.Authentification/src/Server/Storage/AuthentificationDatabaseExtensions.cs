using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pandora.Server.Storage.Authentification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pandora.Server.Storage
{
    public static class AuthentificationDatabaseExtensions
    {
        public static IServiceCollection AddAuthentificationDatabase(this IServiceCollection services, string connectionString)
        {
            services.AddDbContextFactory<AuthentificationDatabaseContext>(o => o.UseNpgsql(connectionString));

            return services;
        }
    }
}
