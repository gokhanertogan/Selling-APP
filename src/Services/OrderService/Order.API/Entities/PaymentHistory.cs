namespace Order.API.Entities;

public class PaymentHistory
{
    public int Id { get; set; }
    public int OrderId { get; set; }
    public string PaymentType { get; set; } = null!;
    public decimal Amount { get; set; }
    public string Date { get; set; } = null!;
}
