using System.Text.Json.Serialization;
using EventBus.Kafka.Abstractions;

namespace Order.API.Messages;

public class DebeziumRootMessage : IIntegrationEvent
{
    [JsonPropertyName("before")]
    public PaymentBefore? Before { get; set; }

    [JsonPropertyName("after")]
    public PaymentAfter? After { get; set; }

    [JsonPropertyName("source")]
    public PaymentSource? Source { get; set; }

    [JsonPropertyName("op")]
    public string? Op { get; set; }

    [JsonPropertyName("ts_ms")]
    public long Ts_ms { get; set; }

    [JsonPropertyName("transaction")]
    public object? Transaction { get; set; }
}

public class PaymentBefore
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("OrderId")]
    public int OrderId { get; set; }

    [JsonPropertyName("PaymentType")]
    public string? PaymentType { get; set; }

    [JsonPropertyName("Amount")]
    public string? Amount { get; set; }
}

public class PaymentAfter
{
    [JsonPropertyName("Id")]
    public int Id { get; set; }

    [JsonPropertyName("OrderId")]
    public int OrderId { get; set; }

    [JsonPropertyName("PaymentType")]
    public string? PaymentType { get; set; }

    [JsonPropertyName("Amount")]
    public string? Amount { get; set; }
}

public class PaymentSource
{
    [JsonPropertyName("version")]
    public string? Version { get; set; }

    [JsonPropertyName("connector")]
    public string? Connector { get; set; }

    [JsonPropertyName("name")]
    public string? Name { get; set; }

    [JsonPropertyName("ts_ms")]
    public long Ts_ms { get; set; }

    [JsonPropertyName("snapshot")]
    public string? Snapshot { get; set; }

    [JsonPropertyName("db")]
    public string? Db { get; set; }

    [JsonPropertyName("sequence")]
    public string? Sequence { get; set; }

    [JsonPropertyName("schema")]
    public string? Schema { get; set; }

    [JsonPropertyName("table")]
    public string? Table { get; set; }

    [JsonPropertyName("txId")]
    public int TxId { get; set; }

    [JsonPropertyName("lsn")]
    public int Lsn { get; set; }

    [JsonPropertyName("xmin")]
    public object? Xmin { get; set; }
}


