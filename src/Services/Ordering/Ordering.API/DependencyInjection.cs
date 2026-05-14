namespace Ordering.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApi(this IServiceCollection services)
        {
            // Add API-specific services here
            return services;
        }

        public static WebApplication UseApiServices(this WebApplication webApplication)
        {
            return webApplication;
        }
    }
}
