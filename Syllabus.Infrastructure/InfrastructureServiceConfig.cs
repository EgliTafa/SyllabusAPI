using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syllabus.Domain.Services.Email;
using Syllabus.Domain.Sylabusses;
using Syllabus.Infrastructure.Data;
using Syllabus.Infrastructure.Repositories;
using Syllabus.Infrastructure.Services.Email;
using Syllabus.Util.Options;


namespace Syllabus.Infrastructure
{
    public static class InfrastructureServiceConfig
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.SectionName));
            services.Configure<ConnectionStringOptions>(configuration.GetSection(ConnectionStringOptions.SectionName));

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("DefaultConnection", "Database connection string is missing.");
            }

            services.AddDbContext<SyllabusDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<ISyllabusRepository, SyllabusRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IBrevoEmailService, BrevoEmailService>();
        }
    }
}
