using Payment.API.Services;
using EventBus.Kafka;
using Microsoft.EntityFrameworkCore;
using Payment.API.Contexts;
using Payment.API.Jobs;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.KafkaServicesRegistration(builder.Configuration);
builder.Services.AddScoped<IPaymentManagementService, PaymentManagementService>();
builder.Services.AddHostedService<PaymentProcessor>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

// app.MapPost("/consume-order-created", (IPaymentManagementService paymentManagementService) =>
// {
//     paymentManagementService.DoPaymentAsync();
//     return Results.Ok("Order created successfully");
// })
// .WithName("ConsumeOrderCreated")
// .WithOpenApi();

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
