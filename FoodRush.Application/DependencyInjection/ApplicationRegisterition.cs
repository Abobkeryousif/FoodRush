namespace FoodRush.Application.DependencyInjection
{
    public static class ApplicationRegisterition
    {
        public static IServiceCollection ApplicationReigster(this IServiceCollection services)
        {
            services.AddMediatR(m=> m.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(typeof(AutoMapping));
            
            return services;
        }
    }
}
