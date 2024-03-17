using EventBus.Kafka;
using EventBus.Kafka.Abstractions;
using Payment.API.Contexts;
using Payment.API.Messages;

namespace Payment.API.Jobs;

public class PaymentProcessor(IConsumerService consumerService, IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly IConsumerService _consumerService = consumerService;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Func<List<OrderCreatedMessage>, Task> processMessageFunc = ProcessMessageAsync;
        await _consumerService.ConsumeAsync(KafkaTopicNameConstant.OrderCreatedTopicName, processMessageFunc);
    }

    public async Task ProcessMessageAsync(List<OrderCreatedMessage> datas)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        datas.ForEach(x => dbContext.Add(new Entities.Payment() { PaymentType = x.PaymentType, Amount = x.TotalAmount, OrderId = x.OrderId }));
        await dbContext.SaveChangesAsync();
    }
}
