using EventBus.Kafka.Abstractions;

namespace Order.API.Messages;

public class OrderCreatedMessage : IIntegrationEvent
{
    public int OrderId { get; set; }
    public string OrderCode { get; set; } = null!;
    public string PaymentType { get; set; } = null!;
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
}