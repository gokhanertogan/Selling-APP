using EventBus.Kafka;
using EventBus.Kafka.Abstractions;
using Order.API.Contexts;
using Order.API.Messages;
using Order.API.Models.Requests;

namespace Order.API.Services;
public class OrderManagementService(IProducerService producerService, IServiceScopeFactory serviceScopeFactory) : IOrderManagementService
{
    private readonly IProducerService _producerService = producerService;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;
    public async Task CreateOrderAsync(OrderCreatedRequestModel requestModel)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        int orderCode = new Random().Next(100000, 999999);

        var addedOrder = new Entities.Order
        {
            OrderCode = orderCode.ToString(),
            CustomerId = requestModel.CustomerId,
            TotalAmount = requestModel.Total
        };

        dbContext.Orders.Add(addedOrder);
        await dbContext.SaveChangesAsync();

        await _producerService.PublishAsync(new OrderCreatedMessage()
        {
            OrderId = addedOrder.Id,
            OrderCode = addedOrder.OrderCode,
            CustomerId = addedOrder.CustomerId,
            TotalAmount = addedOrder.TotalAmount,
            PaymentType ="Credit Card"
        }, KafkaTopicNameConstant.OrderCreatedTopicName);
    }
}