using System.Diagnostics.CodeAnalysis;
using System.Text.Json;
using Confluent.Kafka;
using EventBus.Kafka.Abstractions;

namespace EventBus.Kafka;

public class ProducerService : IProducerService
{
    private readonly ProducerConfig _producerConfig;
    public ProducerService()
    {
        _producerConfig = new ProducerConfig()
        {
            BootstrapServers = "localhost:29092",
            MessageSendMaxRetries = 3,
            // SecurityProtocol = SecurityProtocol.SaslSsl,
            // SaslMechanism = SaslMechanism.Plain,
            // SaslUsername = "",
            // SaslPassword = ""
        };
    }
    public async Task PublishAsync<T>([NotNull] T data, string topicName) where T : IIntegrationEvent
    {
        try
        {
            using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();
            var serializedData = JsonSerializer.Serialize(data);
            await producer.ProduceAsync(topic: topicName, message: new Message<Null, string> { Value = serializedData });
        }
        catch (System.Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}