using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Syllabus.Application.Services;
using Syllabus.Domain.Authentication;
using System.Reflection;

namespace Syllabus.Application
{
    public static class ApplicationServiceConfig
    {
        public static void ConfigureApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddScoped<IRoleManagementService, RoleManagementService>();
        }
    }
}
