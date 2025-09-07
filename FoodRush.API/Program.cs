
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.InfrastructureReigster(builder.Configuration);
builder.Services.ApplicationReigster();
builder.Services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

//Apply Rate Limit 10 request for each IP Address And Block IP 10 Seconds For Save Our API From Dos Attack
builder.Services.AddRateLimiter(options =>
{
    options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    options.AddPolicy("fixed", httpcontext => RateLimitPartition.GetFixedWindowLimiter
    (
        partitionKey: httpcontext.Connection.RemoteIpAddress?.ToString(),
        factory: _=> new FixedWindowRateLimiterOptions
        {
            PermitLimit = 10,
            Window = TimeSpan.FromSeconds(10)
        }));
});

builder.Services.AddControllers()
        .AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<MealDtoValidator>());

//To Handel Error And Deal With Fluent Validation Error And Hide Trace
builder.Services.Configure<ApiBehaviorOptions>(options =>
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


var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRateLimiter();
app.UseAuthorization();

app.MapControllers();

app.Run();
