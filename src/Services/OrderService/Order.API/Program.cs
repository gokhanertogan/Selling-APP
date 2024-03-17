using EventBus.Kafka;
using Microsoft.EntityFrameworkCore;
using Order.API.Contexts;
using Order.API.Jobs;
using Order.API.Models.Requests;
using Order.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.KafkaServicesRegistration(builder.Configuration);
builder.Services.AddScoped<IOrderManagementService, OrderManagementService>();
builder.Services.AddHostedService<OrderPaymentHistoryProcessor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapPost("/create-order", (OrderCreatedRequestModel request, IOrderManagementService orderManagementService) =>
{
    orderManagementService.CreateOrderAsync(request);
    return Results.Ok("Order created successfully");
})
.WithName("PostOrderCreate")
.WithOpenApi();

app.Run();
