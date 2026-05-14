using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration configuration)
        {
            // services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));
             var connectionString = configuration.GetConnectionString("Database");

            //// add services to the container.
            //services.AddDbContext<OrderingDbContext>(options =>
            //    options.UseSqlServer(connectionString));

            // serivices.AddScoped<IOrderingDbContext>(provider => provider.GetService<OrderingDbContext>());

            return services;
        }
    }
}
