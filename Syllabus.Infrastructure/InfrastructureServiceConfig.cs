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
            // Bind connection string from config
            var connectionOptions = new ConnectionStringOptions();
            var emailOptions = new EmailOptions();
            configuration.GetSection(ConnectionStringOptions.SectionName).Bind(connectionOptions);
            configuration.GetSection(EmailOptions.SectionName).Bind(emailOptions);

            if (string.IsNullOrWhiteSpace(connectionOptions.DefaultConnection))
            {
                throw new ArgumentNullException(nameof(connectionOptions.DefaultConnection), "Database connection string is missing.");
            }

            services.AddDbContext<SyllabusDbContext>(options =>
                options.UseSqlServer(connectionOptions.DefaultConnection));

            services.AddScoped<ISyllabusRepository, SyllabusRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IBrevoEmailService, BrevoEmailService>();
        }
    }
}
