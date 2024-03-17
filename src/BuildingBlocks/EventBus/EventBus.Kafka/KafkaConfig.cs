namespace EventBus.Kafka;

public class KafkaConfig
{
    public string BootstrapServers { get; set; } = null!;
    public string SaslMechanism { get; set; } = null!;
    public string SaslUsername { get; set; } = null!;
    public string SaslPassword { get; set; } = null!;
    public string SecurityProtocol { get; set; } = null!;
    public int TryCount { get; set; }
}