using Pandora.Server.Storage;
using Pandora.Server.Storage.Authentification;
using Serilog;

namespace Pandora.WebApiApp.Template
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
            ConfigureApplication(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            var authentificationDatabaseConnectionstring = configuration.GetConnectionString("AuthentificationDatabase") ?? throw new Exception("ConnectionString für AuthentificationDatabase nicht festgelegt");

            services.AddAuthentificationDatabase(authentificationDatabaseConnectionstring);

            services.AddMigratorService(m =>
            {
                m.AddMigration<AuthentificationMigrator>(authentificationDatabaseConnectionstring);
            });
        }

        private static void ConfigureApplication(WebApplication app)
        {
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseMigration();

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
