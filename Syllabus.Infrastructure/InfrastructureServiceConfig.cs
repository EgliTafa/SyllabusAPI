using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syllabus.Infrastructure.Data;
using Syllabus.Util.Options;


namespace Syllabus.Infrastructure
{
    public static class InfrastructureServiceConfig
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Bind connection string from config
            var connectionOptions = new ConnectionStringOptions();
            configuration.GetSection(ConnectionStringOptions.SectionName).Bind(connectionOptions);

            if (string.IsNullOrWhiteSpace(connectionOptions.DefaultConnection))
            {
                throw new ArgumentNullException(nameof(connectionOptions.DefaultConnection), "Database connection string is missing.");
            }

            services.AddDbContext<SyllabusDbContext>(options =>
                options.UseSqlServer(connectionOptions.DefaultConnection));

            // services.AddScoped<ISyllabusRepository, SyllabusRepository>();
        }
    }
}
