using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Pandora.Server.Storages.Authentification;
using Pandora.Server.Storages.Events;
using Pandora.Server.Storages.World;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace Pandora.Server
{
    public static class StorageContextExtensions
    {
        public static IServiceCollection AddAuthentificationStorage(this IServiceCollection services, string? connectionString)
        {
            services.AddStorageContextFactory<IAuthentificationStorageContext, AuthentificationStorageContext>(connectionString);

            return services;
        }

        public static IServiceCollection AddWorldStorage(this IServiceCollection services, string? connectionString)
        {
            services.AddStorageContextFactory<IWorldStorageContext, WorldStorageContext>(connectionString);

            return services;
        }

        public static IServiceCollection AddEventsStorage(this IServiceCollection services, string? connectionString)
        {
            services.AddStorageContextFactory<IEventStorageContext, EventStorageContext>(connectionString);

            return services;
        }

        private static IServiceCollection AddStorageContextFactory<TService, TImplementation>(this IServiceCollection services, string connectionString)
            where TImplementation : DbContext, TService
        {
            if (string.IsNullOrEmpty(connectionString))
            {
                throw new ArgumentException($"\"{nameof(connectionString)}\" kann nicht NULL oder leer sein.", nameof(connectionString));
            }

            var contextOptionServiceDescription = new ServiceDescriptor(
                typeof(DbContextOptions<TImplementation>),
                sp => GetOptions<TImplementation>(options => options.UseNpgsql(connectionString), sp),
                ServiceLifetime.Transient);

            var contextServiceDescription = new ServiceDescriptor(
                 typeof(TImplementation),
                 sp =>
                 {
                     var instance = ActivatorUtilities.CreateInstance<TImplementation>(sp);
                     if (instance is IStorageMigrator migrator)
                     {

                         migrator.Migrate(connectionString);
                     }
                     return instance;
                 },
                  ServiceLifetime.Transient
                );

            services.Add(contextOptionServiceDescription);
            services.Add(contextServiceDescription);

            return services;
        }

        private static object GetOptions<TContext>(Action<DbContextOptionsBuilder>? optionsAction, IServiceProvider sp)
            where TContext : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<TContext>();

            if (sp != null)
            {
                optionsBuilder.UseApplicationServiceProvider(sp);
            }

            optionsAction?.Invoke(optionsBuilder);

            return optionsBuilder.Options;
        }
    }
}
