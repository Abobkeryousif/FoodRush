
namespace FoodRush.Application.DependencyInjection
{
    public static class ApplicationRegisterition
    {
        public static IServiceCollection ApplicationReigster(this IServiceCollection services)
        {
            services.AddMediatR(m => m.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            services.AddTransient(typeof(IPipelineBehavior<,>) , typeof(LoggingBehavior<,>));

            services.AddAutoMapper(typeof(AutoMapping));

            //To Handel Error And Deal With Fluent Validation Error And Hide Trace
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );

                    var result = new
                    {
                        Message = "Validation failed",
                        Errors = errors
                    };

                    return new BadRequestObjectResult(result);
                };
                });
            return services;
        }
    } }
