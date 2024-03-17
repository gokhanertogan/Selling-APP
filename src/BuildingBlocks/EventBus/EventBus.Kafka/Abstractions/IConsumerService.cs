namespace EventBus.Kafka.Abstractions;

public interface IConsumerService
{
    Task ConsumeAsync<T>(string topicName,  Func<List<T>, Task> ProcessMessageAsync) where T : IIntegrationEvent;
}