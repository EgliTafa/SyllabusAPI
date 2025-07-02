using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syllabus.Domain.Services.Email;
using Syllabus.Domain.Services.File;
using Syllabus.Domain.Sylabusses;
using Syllabus.Infrastructure.Data;
using Syllabus.Infrastructure.Repositories;
using Syllabus.Infrastructure.Services.Email;
using Syllabus.Infrastructure.Services.File;
using Syllabus.Util.Options;


namespace Syllabus.Infrastructure
{
    public static class InfrastructureServiceConfig
    {
        public static void AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailOptions>(configuration.GetSection(EmailOptions.SectionName));
            services.Configure<ConnectionStringOptions>(configuration.GetSection(ConnectionStringOptions.SectionName));
            services.Configure<FileStorageOptions>(configuration.GetSection(FileStorageOptions.SectionName));

            var connectionString = configuration.GetConnectionString("DefaultConnection");
            if (string.IsNullOrWhiteSpace(connectionString))
            {
                throw new ArgumentNullException("DefaultConnection", "Database connection string is missing.");
            }

            services.AddDbContext<SyllabusDbContext>(options =>
                options.UseSqlServer(connectionString));

            services.AddScoped<ISyllabusRepository, SyllabusRepository>();
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IProgramRepository, ProgramRepository>();
            services.AddScoped<IBrevoEmailService, BrevoEmailService>();
            services.AddScoped<IFileService, LocalFileService>();
        }
    }
}
