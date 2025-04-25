using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Syllabus.Infrastructure.Data
{
    public class SyllabusDbContextFactory : IDesignTimeDbContextFactory<SyllabusDbContext>
    {
        public SyllabusDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) 
                .AddJsonFile("appsettings.json", optional: false)
                .AddJsonFile("appsettings.Local.json", optional: true)
                .AddEnvironmentVariables()
                .Build();

            var connectionString = configuration.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<SyllabusDbContext>();
            optionsBuilder.UseSqlServer(connectionString);

            return new SyllabusDbContext(optionsBuilder.Options);
        }
    }
}
