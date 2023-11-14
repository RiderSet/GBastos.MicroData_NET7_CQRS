using GBastos.MicroData.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GBastos.MicroData.UI.Web.Configurations
{
    public static class DatabaseConfig
    {
        public static void AddDatabaseConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));

            services.AddDbContext<CTX>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Conn")));

            services.AddDbContext<EventStoreSqlContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Conn")));
        }
    }
}