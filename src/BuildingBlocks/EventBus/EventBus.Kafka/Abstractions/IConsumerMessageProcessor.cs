namespace EventBus.Kafka.Abstractions;

public abstract class IConsumerMessageProcessor
{
    protected delegate bool ProcessMessage(List<IIntegrationEvent> datas);
}