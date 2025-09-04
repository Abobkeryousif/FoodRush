namespace FoodRush.Infrastructure.DependencyInjection
{
    public static class InfrastructureRegistertion
    {
        public static IServiceCollection InfrastructureReigster(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContaxt>(
                option => option.UseNpgsql(configuration.GetConnectionString("Default")));
            return services;
        }
    }
}
