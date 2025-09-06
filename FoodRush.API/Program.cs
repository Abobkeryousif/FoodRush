
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.InfrastructureReigster(builder.Configuration);
builder.Services.ApplicationReigster();

//Apply Rate Limit For Save Our API From Dos Attack
builder.Services.AddRateLimiter( rateLimiterOptions=>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixed" , options=>
    {
        options.PermitLimit = 3;
        options.Window = TimeSpan.FromSeconds(3);
        options.QueueLimit = 0;
    });
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});
var app = builder.Build();

// Configure the HTTP request pipeline.
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
