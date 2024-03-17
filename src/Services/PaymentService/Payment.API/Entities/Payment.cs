namespace Payment.API.Entities;

public class Payment
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string PaymentType { get; set; } = null!;
    public decimal Amount { get; set; }
}
