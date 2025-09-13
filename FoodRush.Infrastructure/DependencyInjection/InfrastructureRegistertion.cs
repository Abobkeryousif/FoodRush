
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
            services.AddScoped(typeof(IMinimalRepository<>), typeof(MinimalRepository<>));
            services.AddScoped<IUnitofwork, Unitofwork>();
            services.AddSingleton<IPhotoService, PhotoService>();
            services.AddTransient<ISendEmailService, SendEmailService>();
            services.AddScoped<ITokenSerivce, TokenService>();
            services.AddScoped<IPaymentService, PaymentService>();

            //Apply Redis Connection
            services.AddSingleton<IConnectionMultiplexer>(i =>
            {
                var conig = ConfigurationOptions.Parse(configuration.GetConnectionString("redis"));
                return ConnectionMultiplexer.Connect(conig);
            });

            services.AddScoped<IOrderService, OrderService>();

            return services;
        }
    }
}
