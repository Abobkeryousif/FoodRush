
namespace FoodRush.Infrastructure.DependencyInjection
{
    public static class InfrastructureRegistertion
    {
        public static IServiceCollection InfrastructureReigster(this IServiceCollection services, IConfiguration configuration)
        {
            //Database Connection
            services.AddDbContext<ApplicationDbContaxt>(
                option => option.UseNpgsql(configuration.GetConnectionString("Default")));

            //Services Reigster
            services.AddScoped(typeof(IGeneraicRepository<>), typeof(GeneraicRepository<>));
            services.AddScoped<IUnitofwork, Unitofwork>();
            services.AddSingleton<IPhotoService, PhotoService>();
            return services;
        }
    }
}
