using EventBus.Kafka;
using EventBus.Kafka.Abstractions;
using Order.API.Contexts;
using Order.API.Messages;

namespace Order.API.Jobs;

public class OrderPaymentHistoryProcessor(IConsumerService consumerService, IServiceScopeFactory serviceScopeFactory) : BackgroundService
{
    private readonly IConsumerService _consumerService = consumerService;
    private readonly IServiceScopeFactory _serviceScopeFactory = serviceScopeFactory;

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        Func<List<DebeziumRootMessage>, Task> processMessageFunc = ProcessMessageAsync;
        await _consumerService.ConsumeAsync(KafkaTopicNameConstant.PaymentChangedTopicName, processMessageFunc);
    }

    public async Task ProcessMessageAsync(List<DebeziumRootMessage> datas)
    {
        using var scope = _serviceScopeFactory.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        datas.ForEach(x => dbContext.Add(new Entities.PaymentHistory() { OrderId = x.After.OrderId, PaymentType = x.After.PaymentType, Amount = Convert.ToDecimal(x.After.Amount), Date = DateTime.Now.ToString() }));
        await dbContext.SaveChangesAsync();
    }
}