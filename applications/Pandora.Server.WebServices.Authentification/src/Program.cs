using EvolveDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Pandora.Server.Storages.Authentification;
using Pandora.Server.WebServices.Authentification.Server.WebServices.Authentification.Services;
using Serilog;
using System.Reflection;

namespace Pandora.Server.WebServices.Authentification
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Configuration.AddCommandLine(args);

            ConfigureLogger(builder);
            ConfigureServices(builder.Services, builder.Configuration);

            var app = builder.Build();

            MigrateDatabase(builder.Configuration.GetConnectionString("AuthentificationDatabase"), app.Logger);

            ConfigureApplication(app);

            app.Run();
        }

        private static void MigrateDatabase(string? connectionString, Microsoft.Extensions.Logging.ILogger logger)
        { 
            try
            {
                var cnx = new NpgsqlConnection(connectionString);
                var evolve = new Evolve(cnx, msg => logger.LogInformation(msg))
                {
                    Locations = new[] { Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Migration", "Authentification") },
                    IsEraseDisabled = true,
                };

                evolve.Migrate();
            }
            catch (Exception ex)
            {
                logger.LogCritical("Database migration failed.", ex);
                throw;
            }
        }

        private static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddTransient<IAccountCreateService, AccountCreateService>();
            services.AddTransient<IAccountReadService, AccountReadService>();

            services.AddTransient<IPasswordService, PasswordService>();

            services.AddDbContext<IAuthentificationStorageContext, AuthentificationStorageContext>(option => option.UseNpgsql(configuration.GetConnectionString("AuthentificationDatabase")));
        }

        private static void ConfigureApplication(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();
        }

        private static void ConfigureLogger(WebApplicationBuilder builder)
        {
            builder.Logging.ClearProviders();

            builder.Host.UseSerilog((hostContext, services, configurtion) =>
            {
                configurtion.WriteTo.Console();
            });
        }
    }
}
