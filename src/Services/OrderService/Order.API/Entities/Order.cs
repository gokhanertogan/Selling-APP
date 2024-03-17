namespace Order.API.Entities;

public class Order
{
    public int Id { get; set; }
    public string OrderCode { get; set; } = null!;
    public int CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
}