using Serilog;

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
            ConfigureApplication(app);

            app.Run();
        }

        private static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddAuthentificationStorage(configuration.GetConnectionString("AuthentificationDatabase"));
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
