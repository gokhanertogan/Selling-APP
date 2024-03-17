using System.Diagnostics.CodeAnalysis;
namespace EventBus.Kafka.Abstractions;

public interface IProducerService
{
    Task PublishAsync<T>([NotNull] T data, string topicName) where T : IIntegrationEvent;
}